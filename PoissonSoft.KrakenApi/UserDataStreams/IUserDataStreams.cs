using System;
using PoissonSoft.KrakenApi.Contracts.PrivateWebSocket;
using PoissonSoft.KrakenApi.Contracts.PublicWebSocket;
using PoissonSoft.KrakenApi.MarketDataStreams;

namespace PoissonSoft.KrakenApi.UserDataStream
{
    public interface IUserDataStreams
    {
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
        SubscriptionInfo SubscribeOnOpenOrders(Action<OHLCPayload> callbackAction);

        /// <summary>
        /// Unsubscribe all subscriptions
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// 
        /// </summary>
        void Close();
    }
}
