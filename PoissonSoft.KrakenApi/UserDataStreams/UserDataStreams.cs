using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using KrakenApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PoissonSoft.KrakenApi.Contracts.MarketDataStream;
using PoissonSoft.KrakenApi.Contracts.PrivateWebSocket;
using PoissonSoft.KrakenApi.Contracts.PrivateWebSocket.Request;
using PoissonSoft.KrakenApi.Contracts.PublicWebSocket;
using PoissonSoft.KrakenApi.Contracts.UserData.Request;
using PoissonSoft.KrakenApi.MarketDataStreams;
using PoissonSoft.KrakenApi.Transport;
using PoissonSoft.KrakenApi.Transport.Rest;
using PoissonSoft.KrakenApi.Transport.Ws;
using PoissonSoft.KrakenApi.UserDataStream;
using PoissonSoft.KrakenApi.Utils;

namespace PoissonSoft.KrakenApi.UserDataStreams
{
    public class UserDataStreams : IUserDataStreams, IDisposable
    {
        private const string WS_ENDPOINT = "wss://ws-auth.kraken.com";
        
        private readonly RestClient RestClient;
        private string token;

        private readonly KrakenApiClient apiClient;
        private readonly WebSocketStreamListener streamListener;

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<long, SubscriptionWrap>> subscriptions =
            new ConcurrentDictionary<string, ConcurrentDictionary<long, SubscriptionWrap>>();


        private readonly string userFriendlyName = nameof(UserDataStreams);
        private TimeSpan reconnectTimeout = TimeSpan.Zero;
        private int ReconnectTimeMultiplier = 1;
        private double TimeReconnectionAgain;

        /// <summary>
        /// Состояние подключения к WebSocket
        /// </summary>
        public DataStreamStatus WsConnectionStatus { get; private set; } = DataStreamStatus.Closed;


        public UserDataStreams(KrakenApiClient apiClient, KrakenApiClientCredentials credentials)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

            RestClient = new RestClient(apiClient.Logger, KrakenApiClient.Endpoint,
                new[] { EndpointSecurityType.Private }, credentials,
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
            ReconnectTimeMultiplier = 1;
            TimeReconnectionAgain = 0;
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
                Open();
                RestoreSubscriptions();
            });
        }

        /// <inheritdoc />
        public void Open()
        {
            if (WsConnectionStatus == DataStreamStatus.Active
                || WsConnectionStatus == DataStreamStatus.Closing
                || WsConnectionStatus == DataStreamStatus.Unknown)
                return;

            WsConnectionStatus = DataStreamStatus.Connecting;

            try
            {
                Thread.Sleep(500 * Convert.ToInt32(TimeReconnectionAgain));
                token = GetToken("0/private/GetWebSocketsToken");
            }
            catch (Exception e)
            {
                if (TimeReconnectionAgain < 120)
                {
                    TimeReconnectionAgain = Math.Pow(ReconnectTimeMultiplier, 2.7);
                    ReconnectTimeMultiplier += 1;
                }

                apiClient.Logger.Error($"{userFriendlyName}. Can not get token. Exception:\n{e}");
                WsConnectionStatus = DataStreamStatus.Closed;
                return;
            }
            TryConnectToWebSocket(token);
        }

        private string GetToken(string tokenStringType)
        {
            var response =
                RestClient.MakeRequest<WebsocketsToken>(
                    new RequestParameters(HttpMethod.Post, tokenStringType, 1, new ReqEmpty()));
            if (string.IsNullOrWhiteSpace(response.Result.Token))
                throw new Exception("Не удалось получить строку token для установки соединения с Websocket");

            if (response.Error == null)
                throw new Exception("Не удалось получить от сервера данные для инициализации соединения с Websocket");

            return response.Result.Token;
        }

        private void TryConnectToWebSocket(string token)
        {
            while (true)
            {
                if (disposed) return;
                try
                {
                    streamListener.Connect($"{WS_ENDPOINT}?token={token}]");
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
        public SubscriptionInfo SubscribeOnOwnTrades(Action<OwnTradesPayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Method = SubscribeMethodName.OwnTrades,
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
        public void AddNewOrder(AddOrderPayloadReq order, Action<AddOrderPayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Method = SubscribeMethodName.AddOrder,
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            if (!subscriptions.TryGetValue(SubscribeMethodName.CancelAll, out _))
            {
                AddSubscription(subscriptionWrap);
            }

            AddOrderToStream(order);
        }

        /// <inheritdoc />
        public void CancelOrder(string[] cancelOrdersId, Action<CancelOrderPayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Method = SubscribeMethodName.CancelOrder,
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            if (!subscriptions.TryGetValue(SubscribeMethodName.CancelAll, out _))
            {
                AddSubscription(subscriptionWrap);
            }
            CancelOrderToStream(cancelOrdersId);
        }

        /// <inheritdoc />
        public void CancelAllOrders(Action<CancelOrderPayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Method = SubscribeMethodName.CancelAll,
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            if (!subscriptions.TryGetValue(SubscribeMethodName.CancelAll, out _))
            {
                AddSubscription(subscriptionWrap);
            }

            CancelAllOrdersToStream();
        }



        private bool AddSubscription(SubscriptionWrap sw)
        {
            var needSubscribeToChannel = false;
            if (!subscriptions.TryGetValue(sw.Info.Method, out var streamSubscriptions))
            {
                streamSubscriptions = new ConcurrentDictionary<long, SubscriptionWrap>();
                if (subscriptions.TryAdd(sw.Info.Method, streamSubscriptions))
                {
                    needSubscribeToChannel = true;
                }
                else
                {
                    subscriptions.TryGetValue(sw.Info.Method, out streamSubscriptions);
                }
            }

            if (streamSubscriptions == null)
                throw new Exception($"{userFriendlyName}. Unexpected error. Can not add key '{sw.Info.SubscribeContent}' " +
                                    $"to {nameof(subscriptions)} dictionary");

            streamSubscriptions[sw.Info.Id] = sw;

            return needSubscribeToChannel;
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
                Open();
                if (WsConnectionStatus != DataStreamStatus.Active)
                    Thread.Sleep(500);
            }

            CommandRequest request = new CommandRequest
            {
                Event = CommandRequestMethod.Subscribe,
                Subscription = new SubscriptionContext
                {
                    Name = subscriptionInfo.Method,
                    Token = token
                },
                RequestId = GenerateUniqueId()
            };
            streamListener.SendMessage(JsonConvert.SerializeObject(request));

            var subscriptionResult = ProcessRequest<object>(request);
            if (subscriptionResult.Success)
            {
                apiClient.Logger.Info($"{userFriendlyName}. Подписка на приветный канал '{request.Subscription.Name}' успешно офомлена");
            }

            return subscriptionResult;
        }

        private void AddOrderToStream(AddOrderPayloadReq order)
        {
            while (!disposed && WsConnectionStatus != DataStreamStatus.Active)
            {
                Open();
                if (WsConnectionStatus != DataStreamStatus.Active)
                    Thread.Sleep(500);
            }

            AddOrderPayloadReq request = new AddOrderPayloadReq
            {
                Event = SubscribeMethodName.AddOrder,
                Token = token,
                OrderType = order.OrderType,
                OrderSide = order.OrderSide,
                Instrument = order.Instrument,
                Price = order.Price,
                Volume = order.Volume,
                TimeInForce = order.TimeInForce,
                RequestId = GenerateUniqueId()
            };
            streamListener.SendMessage(JsonConvert.SerializeObject(request));
        }

        private void CancelOrderToStream(string[] cancelOrdersId)
        {
            while (!disposed && WsConnectionStatus != DataStreamStatus.Active)
            {
                Open();
                if (WsConnectionStatus != DataStreamStatus.Active)
                    Thread.Sleep(500);
            }

            CancelOrderPayloadReq request = new CancelOrderPayloadReq
            {
                Event = SubscribeMethodName.CancelOrder,
                TxId = cancelOrdersId,
                Token = token,
                RequestId = GenerateUniqueId()
            };
            streamListener.SendMessage(JsonConvert.SerializeObject(request));
        }

        private void CancelAllOrdersToStream()
        {
            while (!disposed && WsConnectionStatus != DataStreamStatus.Active)
            {
                Open();
                if (WsConnectionStatus != DataStreamStatus.Active)
                    Thread.Sleep(500);
            }

            CancelOrderPayloadReq request = new CancelOrderPayloadReq
            {
                Event = SubscribeMethodName.CancelAll,
                Token = token,
                RequestId = GenerateUniqueId()
            };
            streamListener.SendMessage(JsonConvert.SerializeObject(request));
        }

        /// <inheritdoc />
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
        public void UnsubscribeAll()
        {
            if (WsConnectionStatus == DataStreamStatus.Active)
            {
                var currentStreams = subscriptions.Keys.ToArray();
                if (!subscriptions.Any())
                    throw new Exception($"{userFriendlyName}. Список активных подписок пуст");

                foreach (var activeStream in currentStreams)
                {
                    var activeSubscriptions = GetDescriptionSubscribe(activeStream);
                    if (activeSubscriptions?.Any() == true)
                    {
                        foreach (var subscription in activeSubscriptions)
                        {
                            var resp = UnsubscribeStream(subscription.Info);
                            if (resp.Success)
                            {
                                apiClient.Logger.Info(
                                    $"{userFriendlyName}. Подписка на приватный канал '{subscription.Info.Method}' успешно отключена");
                            }
                        }
                    }
                }
            }

            subscriptions.Clear();
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
                    Name = subscriptionInfo.Method,
                    Token = token
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

            public AddOrderPayloadReq RequestOrder { get; }

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
                string streamName = jToken[1]?.ToString();
                ProcessPayload(jToken, streamName);
                return;
            }

            var jObject = (JObject)jToken;

            if (jObject.ContainsKey("event"))
            {
                var tMess = jObject["event"]?.ToString();

                if (jObject["event"]?.ToString() == "heartbeat")
                {
                    return;
                }

                // subscription Status
                if (tMess == "subscriptionStatus" && jObject["status"]?.ToString() != "error")
                {
                    ProcessResponse(jObject);
                    return;
                }
            }

            // Ответ на запрос
            if (jObject.ContainsKey("status"))
            {
                if (jObject["status"]?.ToString() == "ok" || jObject["status"]?.ToString() == "error")
                    ProcessPayload(jObject);
                return;
            }


            // Ошибка
            var waitingRequests = responseWaiters.Values.ToArray().Select(x => JsonConvert.SerializeObject(x.Request));

            apiClient.Logger.Error($"{userFriendlyName}. Получена информация об ошибке: {message}\n" +
                                   $"Отправленные запросы, ожидающие ответа:\n{string.Join("\n", waitingRequests)}");
        }

        /// <summary>
        /// Анализ сообщения с ответом на подписку
        /// </summary>
        /// <param name="streamData"></param>
        /// <param name="streamName"></param>
        private void ProcessPayload(JToken streamData, string streamName = null)
        {
            if (streamName == null)
            {
                streamName = streamData["event"]?.ToString();
                var pos = streamName.IndexOf("Status");
                streamName = streamName.Remove(pos, streamName.Length - pos);
            }

            if (string.IsNullOrWhiteSpace(streamName)) return;

            foreach (var subscriptionItem in subscriptions)
            {
                if (subscriptionItem.Key.Contains($"{streamName}"))
                {
                    streamName = subscriptionItem.Key;
                    break;
                }
            }

            if (!subscriptions.TryGetValue(streamName, out var activeSubscriptionsDic))
            {
                apiClient.Logger.Error($"{userFriendlyName}. Получено сообщение из потока '{streamName}', " +
                                       "однако подписок на данный поток не обнаружено");
                return;
            }

            var activeSubscriptions = activeSubscriptionsDic.Values.ToList();
            if (!activeSubscriptions.Any()) return;
            var payloadType = activeSubscriptions.First().Info.Method;
            switch (payloadType)
            {
                case SubscribeMethodName.OwnTrades:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<OwnTradesPayload> callback)
                        {
                            var r = new OwnTradesPayload();
                            try
                            {
                                r.OwnTradeInfo = streamData[0]?.First?.ToObject<Dictionary<string, OwnTradeInfo>>();
                                r.ChannelName = streamData[1]?.Value<string>();
                                r.Sequence = streamData[2]?.First?.ToObject<int>();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            callback(r);
                        }
                    });
                    break;
                case SubscribeMethodName.AddOrder:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<AddOrderPayload> callback)
                        {
                            callback(streamData?.ToObject<AddOrderPayload>());
                        }
                    });
                    break;
                case SubscribeMethodName.CancelOrder:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<CancelOrderPayload> callback)
                        {
                            callback(streamData?.ToObject<CancelOrderPayload>());
                        }
                    });
                    break;
                case SubscribeMethodName.CancelAll:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<CancelOrderPayload> callback)
                        {
                            callback(streamData?.ToObject<CancelOrderPayload>());
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

        /// <inheritdoc />
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
