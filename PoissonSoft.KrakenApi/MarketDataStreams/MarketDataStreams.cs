using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using KrakenApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PoissonSoft.KrakenApi.Contracts.Enums;
using PoissonSoft.KrakenApi.Contracts.MarketDataStream;
using PoissonSoft.KrakenApi.Contracts.PublicWebSocket;
using PoissonSoft.KrakenApi.Transport;
using PoissonSoft.KrakenApi.Transport.Rest;
using PoissonSoft.KrakenApi.Transport.Ws;
using PoissonSoft.KrakenApi.Utils;
using OHLCData = PoissonSoft.KrakenApi.Contracts.PublicWebSocket.OHLCData;
using TickerInfo = PoissonSoft.KrakenApi.Contracts.PublicWebSocket.TickerInfo;

namespace PoissonSoft.KrakenApi.MarketDataStreams
{
    public class MarketDataStreams : IMarketDataStreams, IDisposable
    {
        private const string WS_ENDPOINT = "wss://ws.kraken.com";

        private readonly RestClient RestClient;

        private readonly KrakenApiClient apiClient;
        private readonly WebSocketStreamListener streamListener;

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<long, SubscriptionWrap>> subscriptions =
            new ConcurrentDictionary<string, ConcurrentDictionary<long, SubscriptionWrap>>();


        private readonly string userFriendlyName = nameof(MarketDataStreams);
        private TimeSpan reconnectTimeout = TimeSpan.Zero;

        /// <summary>
        /// Состояние подключения к WebSocket
        /// </summary>
        public DataStreamStatus WsConnectionStatus { get; private set; } = DataStreamStatus.Closed;


        public MarketDataStreams(KrakenApiClient apiClient, KrakenApiClientCredentials credentials)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

            RestClient = new RestClient(apiClient.Logger, KrakenApiClient.Endpoint,
                new[] { EndpointSecurityType.Public }, credentials,
                apiClient.Throttler);

            streamListener = new WebSocketStreamListener(apiClient, credentials);
            streamListener.OnConnected += OnConnectToWs;
            streamListener.OnConnectionClosed += OnDisconnect;
            streamListener.OnMessage += OnStreamMessage;

            WsConnectionStatus = DataStreamStatus.Closed;
        }

        private void OnConnectToWs(object sender, EventArgs e)
        {
            WsConnectionStatus = DataStreamStatus.Active;
            reconnectTimeout = TimeSpan.Zero;
            apiClient.Logger.Info($"{userFriendlyName}. Successfully connected to stream!");
        }

        private void OnDisconnect(object sender, (WebSocketCloseStatus? CloseStatus, string CloseStatusDescription) e)
        {
            if (disposed || WsConnectionStatus == DataStreamStatus.Closing) return;

            WsConnectionStatus = DataStreamStatus.Reconnecting;
            if (reconnectTimeout.TotalSeconds < 15) reconnectTimeout += TimeSpan.FromSeconds(1);
            apiClient.Logger.Error($"{userFriendlyName}. WebSocket was disconnected. Try reconnect again after {reconnectTimeout}.");
            Task.Run(() =>
            {
                Task.Delay(reconnectTimeout);
                TryConnectToWebSocket();
                RestoreSubscriptions();
            });
        }

        private void TryConnectToWebSocket()
        {
            while (true)
            {
                if (disposed) return;
                try
                {
                    streamListener.Connect($"{WS_ENDPOINT}");
                    return;
                }
                catch (Exception e)
                {
                    if (reconnectTimeout.TotalSeconds < 15) reconnectTimeout += TimeSpan.FromSeconds(1);
                    apiClient.Logger.Error($"{userFriendlyName}. WebSocket connection failed. Try again after {reconnectTimeout}. Exception:\n{e}");
                    Thread.Sleep(reconnectTimeout);
                }
            }
        }

        /// <inheritdoc />
        public SubscriptionInfo SubscribeTicker(string[] instruments, Action<TickerPayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Method = SubscribeMethodName.Ticker,
                SubscribeContent = instruments,
                Parameters = new Dictionary<string, string[]>
                {
                    ["pair"] = instruments
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        /// <inheritdoc />
        public SubscriptionInfo SubscribeOHLC(string[] instruments, Action<OHLCPayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Method = SubscribeMethodName.Ohlc,
                SubscribeContent = instruments,
                Parameters = new Dictionary<string, string[]>
                {
                    ["pair"] = instruments
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        /// <inheritdoc />
        public SubscriptionInfo SubscribeTrade(string[] instruments, Action<TradePayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Method = SubscribeMethodName.Trade,
                SubscribeContent = instruments,
                Parameters = new Dictionary<string, string[]>
                {
                    ["pair"] = instruments
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };

            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        /// <inheritdoc />
        public SubscriptionInfo SubscribeSpread(string[] instruments, Action<SpreadPayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Method = SubscribeMethodName.Spread,
                SubscribeContent = instruments,
                Parameters = new Dictionary<string, string[]>
                {
                    ["pair"] = instruments
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        /// <inheritdoc />
        public SubscriptionInfo SubscribeBook(string[] instruments, Action<OrderBookPayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Method = SubscribeMethodName.Book,
                SubscribeContent = instruments,
                Parameters = new Dictionary<string, string[]>
                {
                    ["pair"] = instruments
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        private bool AddSubscription(SubscriptionWrap sw)
        {
            var needSubscribeToChannel = false;
            if (!subscriptions.TryGetValue(ArrayToStr(sw.Info.SubscribeContent), out var streamSubscriptions))
            {
                streamSubscriptions = new ConcurrentDictionary<long, SubscriptionWrap>();
                if (subscriptions.TryAdd(ArrayToStr(sw.Info.SubscribeContent), streamSubscriptions))
                {
                    needSubscribeToChannel = true;
                }
                else
                {
                    subscriptions.TryGetValue(sw.Info.SubscribeContent.ToString(), out streamSubscriptions);
                }
            }

            if (streamSubscriptions == null)
                throw new Exception($"{userFriendlyName}. Unexpected error. Can not add key '{sw.Info.SubscribeContent}' " +
                                    $"to {nameof(subscriptions)} dictionary");

            streamSubscriptions[sw.Info.Id] = sw;

            return needSubscribeToChannel;
        }

        private string ArrayToStr(string[] array)
        {
            string result = null;
            for (int i = 0; i <= array.Length - 1; i++)
            {
                result = result + array[i];
            }
            return result;
        }

        /// <summary>
        /// Восстановление подписок
        /// </summary>
        private void RestoreSubscriptions()
        {
            var currentStreams = subscriptions.Keys.ToArray();
            if (!currentStreams.Any()) return;

            foreach (var activeStream in currentStreams)
            {
                var activeSubscriptions = GetDescriptionSubscribe(activeStream);
                if (activeSubscriptions?.Any() == true)
                {
                    foreach (var subscription in activeSubscriptions)
                    {
                        var resp = SubscribeToStream(subscription.Info);
                        if (!resp.Success)
                        {
                            apiClient.Logger.Error(
                                $"{userFriendlyName}. Не удалось возобновить подписку на stream '{subscription}'. " +
                                $"Ошибка: {resp.ErrorDescription}");
                        }
                    }
                }
            }
        }

        private CommandResponse<object> SubscribeToStream(SubscriptionInfo subscriptionInfo)
        {
            while (!disposed && WsConnectionStatus != DataStreamStatus.Active)
            {
                TryConnectToWebSocket();
                if (WsConnectionStatus != DataStreamStatus.Active)
                    Thread.Sleep(500);
            }

            CommandRequest request = new CommandRequest
            {
                Event = CommandRequestMethod.Subscribe,
                Instruments = subscriptionInfo.SubscribeContent,
                Subscription = new SubscriptionContext
                {
                    Name = subscriptionInfo.Method
                },
                RequestId = GenerateUniqueId()
            };

            var subscriptionResult = ProcessRequest<object>(request);
            if (subscriptionResult.Success)
            {
                apiClient.Logger.Info($"{userFriendlyName}. Подписка на публичный канал '{request.Subscription.Name}' успешно офомлена");
            }

            return subscriptionResult;
        }

        public void Close()
        {
            if (WsConnectionStatus == DataStreamStatus.Closed) return;

            UnsubscribeAll();

            WsConnectionStatus = DataStreamStatus.Closing;
            try
            {
                streamListener?.Close();
            }
            catch (Exception e)
            {
                apiClient.Logger.Error($"{userFriendlyName}. Exception when closing Websocket connection:\n{e}");
            }

            apiClient.Logger.Info($"{userFriendlyName}. Websocket connection closed");
            WsConnectionStatus = DataStreamStatus.Closed;
        }

        /// <inheritdoc />
        public bool Unsubscribe(long subscriptionId)
        {
            var streams = subscriptions.Keys.ToArray();
            foreach (var streamKey in streams)
            {
                if (!subscriptions.TryGetValue(streamKey, out var subscriptionsByKey)
                    || !subscriptionsByKey.ContainsKey(subscriptionId)) continue;

                var t = subscriptionsByKey.Where(x => x.Value.Info.Id == subscriptionId).Select(c => c.Value.Info).ToArray();
                var resp = UnsubscribeStream(t.First());
                if (!subscriptionsByKey.TryRemove(subscriptionId, out _)) return true;

                if (!subscriptionsByKey.IsEmpty) return true;

                if (!subscriptions.TryRemove(streamKey, out _)) return true;

                if (!resp.Success)
                {
                    apiClient.Logger.Error($"{userFriendlyName}. Stream unsubscription error: {resp.ErrorDescription}");
                }

                return resp.Success;
            }

            apiClient.Logger.Error($"{userFriendlyName}. Не удалось отписаться от подписки {subscriptionId}");
            return false;
        }

        /// <inheritdoc />
        public void UnsubscribeAll()
        {
            if (WsConnectionStatus == DataStreamStatus.Active)
            {
                var currentStreams = subscriptions.Keys.ToArray();
                if (!subscriptions.Any())
                    throw new Exception($"{userFriendlyName}. Список активных подписок пуст");

                foreach (var activeSubscription in currentStreams)
                {
                    var activeSubscriptions = GetDescriptionSubscribe(activeSubscription);
                    if (activeSubscriptions?.Any() == true)
                    {
                        foreach (var subscription in activeSubscriptions)
                        {
                            var resp = UnsubscribeStream(subscription.Info);
                            if (resp.Success)
                            {
                                apiClient.Logger.Info(
                                    $"{userFriendlyName}. Подписка на публичный канал '{subscription.Info.Method}' успешно отключена");
                            }
                        }
                    }
                }
            }

            subscriptions.Clear();
        }

        private CommandResponse<object> UnsubscribeStream(SubscriptionInfo subscriptionInfo)
        {
            if (WsConnectionStatus != DataStreamStatus.Active) return new CommandResponse<object>
            {
                Success = true
            };

            CommandRequest request = new CommandRequest
            {
                Event = CommandRequestMethod.Unsubscribe,
                Instruments = subscriptionInfo.SubscribeContent,
                Subscription = new SubscriptionContext
                {
                    Name = subscriptionInfo.Method
                },
                RequestId = GenerateUniqueId()
            };

            return ProcessRequest<object>(request);
        }

        private class SubscriptionWrap
        {
            public SubscriptionInfo Info { get; set; }
            public object CallbackAction { get; set; }
        }

        private long lastId;
        private long GenerateUniqueId()
        {
            return Interlocked.Increment(ref lastId);
        }

        #region [Request processing]

        private class CommandResponse<T>
        {
            public bool Success { get; set; }

            public T Data { get; set; }

            public string ErrorDescription { get; set; }
        }

        private class ManualResetEventPool : ObjectPool<ManualResetEventSlim>
        {
            public ManualResetEventPool()
                : base(startSize: 20, minSize: 10, sizeIncrement: 5, availableLimit: 200)
            { }

            // Новый экземпляр объекта создаётся в не сигнальном состоянии
            protected override ManualResetEventSlim CreateEntity()
            {
                return new ManualResetEventSlim();
            }
        }

        private class ResponseWaiter
        {
            public CommandRequest Request { get; }

            public ManualResetEventSlim SyncEvent { get; }

            public CommandResponse<object> Response { get; set; }

            public ResponseWaiter(CommandRequest request, ManualResetEventSlim syncEvent)
            {
                Request = request;
                SyncEvent = syncEvent;
            }
        }

        private readonly ManualResetEventPool manualResetEventPool = new ManualResetEventPool();

        /// <summary>
        /// Время ожидания (в миллисекундах) обработки отправленного запроса
        /// </summary>
        private const int WAIT_FOR_REQUEST_PROCESSING_MS = 10_000;

        private readonly ConcurrentDictionary<long, ResponseWaiter> responseWaiters =
            new ConcurrentDictionary<long, ResponseWaiter>();

        private CommandResponse<T> ProcessRequest<T>(CommandRequest request)
        {
            bool requestWasProcessed;
            ManualResetEventSlim syncEvent = null;
            ResponseWaiter waiter = null;
            try
            {
                if (!manualResetEventPool.TryGetEntity(out syncEvent))
                {
                    apiClient.Logger.Error($"{userFriendlyName}. Couldn't get the synchronization object from the pool");
                }
                syncEvent?.Reset();
                waiter = new ResponseWaiter(request, syncEvent);
                responseWaiters.TryAdd(waiter.Request.RequestId, waiter);
                streamListener.SendMessage(JsonConvert.SerializeObject(request));

                requestWasProcessed = syncEvent != null && syncEvent.Wait(WAIT_FOR_REQUEST_PROCESSING_MS);
            }
            finally
            {
                if (syncEvent != null) manualResetEventPool.ReturnToPool(syncEvent);
                if (waiter?.Request != null) responseWaiters.TryRemove(waiter.Request.RequestId, out _);
            }

            if (!requestWasProcessed)
            {
                return new CommandResponse<T>
                {
                    Success = false,
                    ErrorDescription = "A response to the request has not been received within the " +
                                       $"specified timeout ({WAIT_FOR_REQUEST_PROCESSING_MS} мс)"
                };
            }

            if (waiter.Response?.Success == null)
            {
                return new CommandResponse<T>
                {
                    Success = false,
                    ErrorDescription = "Incorrect response: " +
                                       $"{(waiter.Response == null ? "NULL" : JsonConvert.SerializeObject(waiter.Response))}"
                };
            }

            if (waiter.Response?.Success == false)
            {
                return new CommandResponse<T>
                {
                    Success = false,
                    ErrorDescription = waiter.Response.ErrorDescription ?? "Error"
                };
            }

            T data;
            try
            {
                data = (T)waiter.Response.Data;
            }
            catch
            {
                return new CommandResponse<T>
                {
                    Success = false,
                    ErrorDescription = "Incorrect data in Response: " +
                                       $"{(waiter.Response.Data == null ? "NULL" : waiter.Response.Data.GetType().Name)}"
                };
            }

            return new CommandResponse<T>
            {
                Success = true,
                ErrorDescription = waiter.Response.ErrorDescription,
                Data = data
            };

        }


        #endregion

        #region [Payload processing]

        private void OnStreamMessage(object sender, string message)
        {
            Task.Run(() =>
            {
                try
                {
                    ProcessStreamMessage(message);
                }
                catch (Exception e)
                {
                    apiClient.Logger.Error($"{userFriendlyName}. Exception when processing payload.\n" +
                                 $"Message: {message}\n" +
                                 $"Exception: {e}");
                }
            });
        }

        /// <summary>
        /// Данный метод, выполняет функцию фильтра, определяя какой тип ответа пришел и куда направить дальше.
        /// </summary>
        /// <param name="message"></param>
        private void ProcessStreamMessage(string message)
        {
            if (apiClient.IsDebug)
            {
                apiClient.Logger.Debug($"{userFriendlyName}. New payload received:\n{message}");
            }
            var jToken = JToken.Parse(message);

            // Payload
            if (jToken.Type == JTokenType.Array)
            {
                ProcessPayload(jToken);
                return;
            }

            var jObject = (JObject)jToken;

            // Ответ на запрос
            if (jObject.ContainsKey("event"))
            {
                var tMess = jObject["event"]?.ToString();

                if (tMess == "heartbeat")
                {

                }

                else if (tMess == "subscriptionStatus" && jObject["status"]?.ToString() == "error")
                {
                    apiClient.Logger.Debug($"{userFriendlyName}. New payload received:\n{JsonConvert.SerializeObject(jObject.ToObject<ResponseMessage>(), Formatting.Indented)}");
                    return;
                }

                // subscribed
                else if (tMess == "subscriptionStatus" && jObject["status"]?.ToString() == "subscribed")
                {
                    ProcessResponse(jObject);
                }

                // unsubscribed
                else if (tMess == "subscriptionStatus" && jObject["status"]?.ToString() == "unsubscribed")
                {
                    ProcessResponse(jObject);
                }
                return;
            }

            // Ошибка
            var waitingRequests = responseWaiters.Values.ToArray().Select(x => JsonConvert.SerializeObject(x.Request));

            apiClient.Logger.Error($"{userFriendlyName}. Получена информация об ошибке: {message}\n" +
                                   $"Отправленные запросы, ожидающие ответа:\n{string.Join("\n", waitingRequests)}");
        }

        private List<SubscriptionWrap> GetDescriptionSubscribe(string subscribeKey)
        {
            if (string.IsNullOrWhiteSpace(subscribeKey)) new List<SubscriptionWrap>();

            foreach (var subscriptionItem in subscriptions)
            {
                if (subscriptionItem.Key.Contains($"{subscribeKey}"))
                {
                    subscribeKey = subscriptionItem.Key;
                    break;
                }
            }

            if (!subscriptions.TryGetValue(subscribeKey, out var activeSubscriptionsDic))
            {
                apiClient.Logger.Error($"{userFriendlyName}. Получено сообщение из потока '{subscribeKey}', " +
                                       "однако подписок на данный поток не обнаружено");
                return new List<SubscriptionWrap>();
            }

            return activeSubscriptionsDic.Values.ToList();
        }

        /// <summary>
        /// Анализ сообщения с ответом на подписку
        /// </summary>
        /// <param name="streamData"></param>
        private void ProcessPayload(JToken streamData)
        {
            var r0 = new TickerPayload();
            r0.Instrument = streamData[3]?.ToString();

            var activeSubscriptions = GetDescriptionSubscribe(r0.Instrument);
            if (!activeSubscriptions.Any()) return;
            var payloadType = activeSubscriptions.First().Info.Method;
            switch (payloadType)
            {
                case SubscribeMethodName.Ticker:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<TickerPayload> callback)
                        {
                            var r = new TickerPayload();
                            try
                            {
                                r.ChannelID = Convert.ToInt32(streamData[0]);
                                r.TickerInfo = new TickerInfo();
                                r.TickerInfo = streamData[1]?.ToObject<TickerInfo>();
                                r.channelName = streamData[2]?.ToString();
                                r.Instrument = streamData[3]?.ToString();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }

                            callback(r);
                        }
                    });
                    break;
                case SubscribeMethodName.Ohlc:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<OHLCPayload> callback)
                        {
                            var r = new OHLCPayload();
                            try
                            {
                                r.ChannelID = Convert.ToInt32(streamData[0]);
                                r.OHLCData = new OHLCData[1];
                                var itemContent = streamData[1].ToObject<decimal[]>();
                                r.OHLCData[0] = new OHLCData();
                                r.OHLCData[0].Time = itemContent[0];
                                r.OHLCData[0].Etime = itemContent[1];
                                r.OHLCData[0].OpenPrice = itemContent[2];
                                r.OHLCData[0].HighPrice = itemContent[3];
                                r.OHLCData[0].LowPrice = itemContent[4];
                                r.OHLCData[0].ClosePrice = itemContent[5];
                                r.OHLCData[0].VolumeWAP = itemContent[6];
                                r.OHLCData[0].Volume = itemContent[7];
                                r.OHLCData[0].Count = (int)itemContent[8];

                                r.ChannelName = streamData[2]?.ToString();
                                r.Instrument = streamData[3]?.ToString();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }

                            callback(r);
                        }
                    });
                    break;

                case SubscribeMethodName.Trade:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<TradePayload> callback)
                        {
                            var r = new TradePayload();
                            r.ChannelID = Convert.ToInt32(streamData[0]);

                            var tradeInfoItems = streamData[1].ToArray();
                            r.TradeInfo = new TradeInfo[tradeInfoItems.Length][];

                            for (int j = 0; j < tradeInfoItems.Length; j++)
                            {
                                var itemContent = tradeInfoItems[j].ToObject<string[]>();
                                r.TradeInfo[j] = new TradeInfo[tradeInfoItems.Length];
                                for (int i = 0; i < tradeInfoItems.Length; i++)
                                {
                                    r.TradeInfo[j][i] = new TradeInfo();
                                    r.TradeInfo[j][i].Price = Convert.ToDecimal(itemContent[0].Replace(".", ","));
                                    r.TradeInfo[j][i].Volume = Convert.ToDecimal(itemContent[1].Replace(".", ","));
                                    r.TradeInfo[j][i].Time = Convert.ToDecimal(itemContent[2].Replace(".", ","));
                                    r.TradeInfo[j][i].OrderSide = OrderSideConverter(itemContent[3]);
                                    r.TradeInfo[j][i].OrderType = OrderTypeConverter(itemContent[4]);
                                    r.TradeInfo[j][i].Misc = itemContent[5];
                                }
                            }

                            r.ChannelName = streamData[2]?.ToString();
                            r.Instrument = streamData[3]?.ToString();

                            callback(r);
                        }
                    });
                    break;

                case SubscribeMethodName.Spread:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<SpreadPayload> callback)
                        {
                            var r = new SpreadPayload();
                            r.ChannelID = Convert.ToInt32(streamData[0]);

                            r.SpreadInfo = new SpreadInfo[1];
                            var itemContent = streamData[1].ToObject<decimal[]>();

                            r.SpreadInfo[0] = new SpreadInfo();
                            r.SpreadInfo[0].Ask = itemContent[0];
                            r.SpreadInfo[0].Bid = itemContent[1];
                            r.SpreadInfo[0].Timestamp = itemContent[2];
                            r.SpreadInfo[0].BidVolume = itemContent[3];
                            r.SpreadInfo[0].AskVolume = itemContent[4];

                            r.ChannelName = streamData[2]?.ToString();
                            r.Instrument = streamData[3]?.ToString();
                            callback(r);
                        }
                    });
                    break;

                case SubscribeMethodName.Book:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<OrderBookPayload> callback)
                        {
                            var r = new OrderBookPayload();
                            r.ChannelID = Convert.ToInt32(streamData[0]);

                            var orderBookItems = streamData[1].ToArray();
                            r.Orderbook = new OrkerBook[1];

                            try
                            {
                                // Asks update
                                if (orderBookItems.First().Path.Contains("a") && !orderBookItems.First().Path.Contains("as"))
                                {
                                    r.Orderbook[0] = new OrkerBook();
                                    var askContent = streamData[1].First().First?.ToObject<decimal[][]>();
                                    r.Orderbook[0].Ask = new OrderAsk[askContent.Length];
                                    //r.Orderbook[0].Bid = new OrderAsk[askContent.Length];
                                    for (int i = 0; i < askContent.Length; i++)
                                    {
                                        r.Orderbook[0].Ask[i] = new OrderAsk();
                                        r.Orderbook[0].Ask[i].Price = askContent[i][0];
                                        r.Orderbook[0].Ask[i].Volume = askContent[i][1];
                                        r.Orderbook[0].Ask[i].Time = askContent[i][2];

                                        //r.Orderbook[0].Bid[i] = new OrderAsk();
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                            }

                            try
                            {
                                // Bids update
                                if (orderBookItems.First().Path.Contains("b") && !orderBookItems.First().Path.Contains("bs"))
                                {
                                    r.Orderbook[0] = new OrkerBook();
                                    var bidContent = streamData[1].First().First?.ToObject<decimal[][]>();
                                    //r.Orderbook[0].Ask = new OrderAsk[bidContent.Length];
                                    r.Orderbook[0].Bid = new OrderAsk[bidContent.Length];
                                    for (int i = 0; i < bidContent.Length; i++)
                                    {
                                        //r.Orderbook[0].Ask[i] = new OrderAsk();

                                        r.Orderbook[0].Bid[i] = new OrderAsk();
                                        r.Orderbook[0].Bid[i].Price = bidContent[i][0];
                                        r.Orderbook[0].Bid[i].Volume = bidContent[i][1];
                                        r.Orderbook[0].Bid[i].Time = bidContent[i][2];
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                            }


                            // full snapshot
                            if (orderBookItems.First().Path.Contains("as") ||
                                orderBookItems.First().Path.Contains("bs"))
                            {
                                for (int j = 0; j < 1; j++)
                                {
                                    var askContent = orderBookItems[j].First?.ToObject<decimal[][]>();
                                    var bidContent = orderBookItems[j + 1].First?.ToObject<decimal[][]>();
                                    r.Orderbook[j] = new OrkerBook();
                                    r.Orderbook[j].Ask = new OrderAsk[askContent.Length];
                                    r.Orderbook[j].Bid = new OrderAsk[askContent.Length];

                                    for (int i = 0; i < askContent.Length; i++)
                                    {
                                        // Asks
                                        r.Orderbook[j].Ask[i] = new OrderAsk();
                                        r.Orderbook[j].Ask[i].Price = askContent[i][0];
                                        r.Orderbook[j].Ask[i].Volume = askContent[i][1];
                                        r.Orderbook[j].Ask[i].Time = askContent[i][2];
                                        // Bids
                                        r.Orderbook[j].Bid[i] = new OrderAsk();
                                        r.Orderbook[j].Bid[i].Price = bidContent[i][0];
                                        r.Orderbook[j].Bid[i].Volume = bidContent[i][1];
                                        r.Orderbook[j].Bid[i].Time = bidContent[i][2];
                                    }
                                }
                            }
                            r.ChannelName = streamData[2]?.ToString();
                            r.Instrument = streamData[3]?.ToString();
                            callback(r);
                        }
                    });
                    break;

                default:
                    {
                        apiClient.Logger.Error($"{userFriendlyName}. Unknown Payload type '{payloadType}'");
                        break;
                    }
            }
        }

        private OrderSide OrderSideConverter(string orderSide)
        {
            if (orderSide.Contains("b"))
                return OrderSide.Buy;
            if (orderSide.Contains("s"))
                return OrderSide.Sell;

            return OrderSide.Unknown;
        }

        private OrderType OrderTypeConverter(string orderSide)
        {
            if (orderSide.Contains("m"))
                return OrderType.Market;
            if (orderSide.Contains("l"))
                return OrderType.Limit;

            return OrderType.Unknown;
        }

        private void ProcessResponse(JObject response)
        {
            long requestId;
            try
            {
                requestId = response["reqid"]?.ToObject<long>()
                            ?? throw new Exception("При конвертации поля id в long получили значение NULL");
            }
            catch (Exception ex)
            {
                apiClient.Logger.Error($"{userFriendlyName}. При обработке ответа на запрос не удалось получить ID запроса. " +
                                       $"Message: {response}\nException {ex}");
                return;
            }

            if (!responseWaiters.TryGetValue(requestId, out var waiter))
            {
                apiClient.Logger.Error($"{userFriendlyName}. Среди ожидающих ответа запросов не удалось найти запрос " +
                                       $"с ID={requestId}");
                return;
            }

            var cmdResp = new CommandResponse<object>
            {
                Success = true
            };
            try
            {
                switch (waiter.Request.Event)
                {
                    case CommandRequestMethod.Subscribe:
                    case CommandRequestMethod.Unsubscribe:
                    case CommandRequestMethod.Ping:
                        cmdResp.Data = null;
                        break;
                    default:
                        cmdResp.Success = false;
                        cmdResp.ErrorDescription = $"Unknown Request Method '{waiter.Request.Event}'";
                        break;
                }
            }
            catch (Exception e)
            {
                cmdResp.Success = false;
                cmdResp.ErrorDescription = e.Message;
            }

            waiter.Response = cmdResp;
            waiter.SyncEvent.Set();
        }

        #endregion


        private bool disposed;

        public void Dispose()
        {
            if (disposed) return;

            if (streamListener != null)
            {
                Close();
                streamListener.OnConnected -= OnConnectToWs;
                streamListener.OnConnectionClosed -= OnDisconnect;
                streamListener.OnMessage -= OnStreamMessage;
                streamListener?.Dispose();
            }
            RestClient?.Dispose();
            manualResetEventPool?.Dispose();

            disposed = true;

            GC.SuppressFinalize(this);
        }
    }
}
