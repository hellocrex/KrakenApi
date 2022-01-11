using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using KrakenApi;
using PoissonSoft.KrakenApi.Contracts;
using PoissonSoft.KrakenApi.Contracts.Enums;
using PoissonSoft.KrakenApi.Contracts.Exceptions;
using PoissonSoft.KrakenApi.Utils;

namespace PoissonSoft.KrakenApi.Transport
{
    internal class Throttler: IDisposable
    {
        private readonly KrakenApiClient apiClient;

        private readonly string userFriendlyName = nameof(Throttler);
        private readonly WaitablePool syncPool;
        private readonly WaitablePool syncPoolWs;

        private List<FeedLocker> actualFeedLockerCounter;

        private Dictionary<FeedLocker, DateTimeOffset> actualFeedLockerDictionary =
            new Dictionary<FeedLocker, DateTimeOffset>();
        // "Стоимость" одного бала в миллисекундах для каждого из параллельно исполняемых REST-запросов.
        // Т.е. если в конкретном потоке (одном из всех MaxDegreeOfParallelism параллельных) выполняется запрос
        // с весом 1 балл, то этот поток не должен проводить новых запросов в течение requestWeightCostInMs миллисекунд
        private int weightUnitCostInMs;

        // "Стоимость" одного бала в миллисекундах для каждого из параллельно исполняемых WebSocket-запросов.
        private readonly int wsWeightUnitCostInMs;

        // Значение по умолчанию для времени приостановки запросов после превышения лимита (в секундах)
        private int defaultRetryAfterSec = 60;

        // Время (UTC), до которого приостановлены все запросы в связи с превышением лимита
        private object rateLimitPausedTime = DateTimeOffset.MinValue;

        /// <summary>
        /// Максимальное количество параллельно выполняемых запросов
        /// </summary>
        public int MaxDegreeOfParallelism { get; }

        /// <summary>
        /// Количество Feeds, выделяемых под высоко-приоритетные запросы
        /// Значение должно быть строго меньше чем <see cref="MaxDegreeOfParallelism"/>
        /// </summary>
        public int HighPriorityFeedsCount { get; }

        /// <summary>
        /// Create instance
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="maxDegreeOfParallelism">Максимальное допустимое количество параллельно выполняемых запросов</param>
        /// <param name="highPriorityFeedsCount">Количество feeds, выделяемое под исполнение запросов с высоким приоритетом</param>
        public Throttler(KrakenApiClient apiClient, int maxDegreeOfParallelism = 5, int highPriorityFeedsCount = 1)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            MaxDegreeOfParallelism = maxDegreeOfParallelism;
            if (MaxDegreeOfParallelism < 1) MaxDegreeOfParallelism = 1;

            HighPriorityFeedsCount = highPriorityFeedsCount;
            if (HighPriorityFeedsCount >= MaxDegreeOfParallelism)
                HighPriorityFeedsCount = MaxDegreeOfParallelism - 1;

            actualFeedLockerCounter = new List<FeedLocker>();

            syncPool = new WaitablePool(MaxDegreeOfParallelism, highPriorityFeedsCount);

            const int WS_MaxDegreeOfParallelism = 5;
            // WebSocket connections have a limit of 5 incoming messages per second.
            const int WS_RequestLimitPerSecond = 5;
            syncPoolWs = new WaitablePool(WS_MaxDegreeOfParallelism, 0);
            wsWeightUnitCostInMs = WS_MaxDegreeOfParallelism * 1000 / WS_RequestLimitPerSecond;
        }

        /// <summary>
        /// Применить актуальные лимиты
        /// </summary>
        /// <param name="maxWeigthRequest"></param>
        public void CalculateWeightUnitCost( int maxWeigthRequest)
        {
            var minWeightPerSecondLimit = (maxWeigthRequest / 0.33) * 1000;
            Interlocked.Exchange(ref weightUnitCostInMs, (int)minWeightPerSecondLimit);
        }

        /// <summary>
        /// REST-request throttling
        /// </summary>
        /// <param name="requestWeight"></param>
        /// <param name="highPriority"></param>
        /// <param name="isOrderRequest"></param>
        public void ThrottleRest(int requestWeight, bool highPriority, bool isOrderRequest)
        {
            CalculateWeightUnitCost(requestWeight);

            var dt = DateTimeOffset.UtcNow;
            var locker = syncPool.Wait(highPriority);

            var tmpLockers = new FeedLocker[actualFeedLockerCounter.Count];
            actualFeedLockerCounter.CopyTo(tmpLockers);

            locker.UnlockAfterMs(weightUnitCostInMs * (actualFeedLockerCounter.Count + 1));

            if (actualFeedLockerDictionary.ContainsKey(locker) && actualFeedLockerDictionary[locker] <= DateTimeOffset.Now)
            {
                actualFeedLockerCounter.Remove(locker);
                actualFeedLockerDictionary.Remove(locker);
            }

            try
            {
                if (!actualFeedLockerDictionary.ContainsKey(locker))
                {
                    actualFeedLockerCounter.Add(locker);
                    actualFeedLockerDictionary.Add(locker, dt.AddMilliseconds(weightUnitCostInMs * (actualFeedLockerCounter.Count + 1)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            var waitTime = (DateTimeOffset.UtcNow - dt).TotalSeconds;
            if (waitTime > 7)
            {
                apiClient.Logger.Warn($"{userFriendlyName}. Время ожидания тротлинга REST-запроса составило {waitTime:F0} секунд. " +
                                      "Возможно, следует оптимизировать прикладные алгоритмы с целью сокращения количества запросов");
            }

            // Здесь не используем Interlocked для чтения rateLimitPausedTime по следующим соображениям:
            // - кривое значение будет прочитано в исключительно редких случаях, при этом возможно будет пропущен запрос, который следовало пресечь, или
            //   остановлен запрос, который следовало пропустить. Это не является большой проблемой
            // - в подавляющем большинстве случаев запрос будет пропускаться, при этом лишняя задержка на Interlocked операцию здесь выглядит совсем лишней
            if (DateTimeOffset.UtcNow < (DateTimeOffset)rateLimitPausedTime)
                throw new RequestRateLimitBreakingException($"All requests banned until {(DateTimeOffset)rateLimitPausedTime}");
        }

        /// <summary>
        /// WebSocket send message throttling
        /// 
        /// </summary>
        /// <param name="requestWeight"></param>
        public void ThrottleWs(int requestWeight)
        {
            var dt = DateTimeOffset.UtcNow;
            var locker = syncPool.Wait(false);
            locker.UnlockAfterMs(requestWeight * wsWeightUnitCostInMs);
            var waitTime = (DateTimeOffset.UtcNow - dt).TotalSeconds;
            if (waitTime > 5)
            {
                apiClient.Logger.Warn($"{userFriendlyName}. Время ожидания тротлинга WebSocket-запроса составило {waitTime:F0} секунд. " +
                                      "Возможно, следует оптимизировать прикладные алгоритмы с целью сокращения количества запросов");
            }
        }

        /// <summary>
        /// Apply response headers.
        /// IP Limits
        ///     Every request will contain X-MBX-USED-WEIGHT-(intervalNum)(intervalLetter) in the response headers which
        ///     has the current used weight for the IP for all request rate limiters defined.
        /// Order Rate Limits
        ///     Every successful order response will contain a X-MBX-ORDER-COUNT-(intervalNum)(intervalLetter) header which
        ///     has the current order count for the account for all order rate limiters defined.
        /// </summary>
        /// <param name="headers"></param>
        public void ApplyRestResponseHeaders(HttpResponseHeaders headers)
        {
            // TODO:
            // Not implemented yet
        }

        /// <summary>
        /// HTTP 429 return code is used when breaking a request rate limit.
        /// HTTP 418 return code is used when an IP has been auto-banned for continuing to send requests after receiving 429 codes.
        /// A Retry-After header is sent with a 418 or 429 responses and will give the number of seconds required to wait, in the case of a 429,
        /// to prevent a ban, or, in the case of a 418, until the ban is over.
        /// </summary>
        /// <param name="retryAfter">Время приостановки приёма запросов</param>
        public void StopAllRequestsDueToRateLimit(TimeSpan? retryAfter)
        {
            var timeout = retryAfter ?? TimeSpan.FromSeconds(defaultRetryAfterSec);

            var pausedTime = DateTimeOffset.UtcNow + timeout;

            Interlocked.Exchange(ref rateLimitPausedTime, pausedTime);
        }

        private readonly SimpleScheduler ssUpdateLimits = new SimpleScheduler(TimeSpan.FromMinutes(10));
        private readonly object syncUpdateLimits = new object();

        public void Dispose()
        {
            syncPool?.Dispose();
            syncPoolWs?.Dispose();
        }
    }
}
