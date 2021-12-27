using System;
using PoissonSoft.KrakenApi.Contracts.PrivateWebSocket;
using PoissonSoft.KrakenApi.Contracts.PublicWebSocket;
using PoissonSoft.KrakenApi.MarketDataStreams;
using PoissonSoft.KrakenApi.Transport;

namespace PoissonSoft.KrakenApi.UserDataStream
{
    public interface IUserDataStreams
    {

        DataStreamStatus WsConnectionStatus { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callbackAction"></param>
        /// <returns></returns>
        SubscriptionInfo SubscribeOnOwnTrades(Action<OwnTradesPayload> callbackAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callbackAction"></param>
        /// <returns></returns>
        SubscriptionInfo AddNewOrder(Action<OHLCPayload> callbackAction);

        /// <summary>
        /// Unsubscribe all subscriptions
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// 
        /// </summary>
        void Open();

        /// <summary>
        /// 
        /// </summary>
        void Close();
    }
}
