using System;
using PoissonSoft.KrakenApi.Contracts.PrivateWebSocket;
using PoissonSoft.KrakenApi.Contracts.PrivateWebSocket.Request;
using PoissonSoft.KrakenApi.MarketDataStreams;
using PoissonSoft.KrakenApi.Transport;

namespace PoissonSoft.KrakenApi.UserDataStreams
{
    public interface IUserDataStreams
    {

        DataStreamStatus WsConnectionStatus { get; }

        /// <summary>
        /// Own trades. On subscription last 50 trades for the user will be sent, followed by new trades.
        /// </summary>
        /// <param name="callbackAction"></param>
        /// <returns></returns>
        SubscriptionInfo SubscribeOnOwnTrades(Action<OwnTradesPayload> callbackAction);

        /// <summary>
        /// Add new order.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="callbackAction"></param>
        /// <returns></returns>
        void AddNewOrder(AddOrderPayloadReq request, Action<AddOrderPayload> callbackAction);

        /// <summary>
        /// Cancel order or list of orders.
        /// </summary>
        /// <param name="cancelOrdersId"></param>
        /// <param name="callbackAction"></param>
        void CancelOrder(string[] cancelOrdersId, Action<CancelOrderPayload> callbackAction);

        /// <summary>
        /// Cancel all open orders. Includes partially-filled orders.
        /// </summary>
        /// <param name="callbackAction"></param>
        void CancelAllOrders(Action<CancelOrderPayload> callbackAction);

        /// <summary>
        /// Unsubscribe all subscriptions
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// Get private token and open WS connection
        /// </summary>
        void Open();

        /// <summary>
        /// Close WS connection
        /// </summary>
        void Close();
    }
}
