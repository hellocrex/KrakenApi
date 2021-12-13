using System;
using System.Linq;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Filters;
using PoissonSoft.KrakenApi.Contracts.Serialization;

namespace PoissonSoft.KrakenApi.Contracts
{
    /// <summary>
    /// Общие данные по бирже
    /// </summary>
    public class ExchangeInfo : ICloneable
    {
        /// <summary>
        /// Таймзона сервера биржи
        /// </summary>
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        /// <summary>
        /// Серверное время
        /// </summary>
        [JsonProperty("serverTime")]
        public long ServerTime { get; set; }

        /// <summary>
        /// Действующие лимиты (по запросам и по ордерам)
        /// </summary>
        [JsonProperty("rateLimits")]
        public RateLimit[] RateLimits { get; set; }

        /// <summary>
        /// Фильтры
        /// </summary>
        [JsonProperty(PropertyName = "filters", ItemConverterType = typeof(FilterConverter))]
        public ExchangeFilter[] Filters { get; set; }
        /// <inheritdoc />
        public object Clone()
        {
            return new ExchangeInfo
            {
                Timezone = Timezone,
                ServerTime = ServerTime,
                RateLimits = RateLimits?.Select(x => (RateLimit)x.Clone()).ToArray(),
                Filters = Filters?.Select(x => (ExchangeFilter)x.Clone()).ToArray()
            };
        }
    }
}
