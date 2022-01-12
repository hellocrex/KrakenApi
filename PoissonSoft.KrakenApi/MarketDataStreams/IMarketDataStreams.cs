using System;
using PoissonSoft.KrakenApi.Contracts.PublicWebSocket;

namespace PoissonSoft.KrakenApi.MarketDataStreams
{
    public interface IMarketDataStreams
    {
        /// <summary>
        /// Unsubscribe all subscriptions
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// Ticker information on currency pair.
        /// </summary>
        SubscriptionInfo SubscribeTicker(string[] instruments, Action<TickerPayload> callbackAction);

        /// <summary>
        /// When subscribed for OHLC, a snapshot of the last valid candle (irrespective of the endtime) will be sent,
        /// followed by updates to the running candle. For example, if a subscription is made to 1 min candle and there have been no trades for 5 mins,
        /// a snapshot of the last 1 min candle from 5 mins ago will be published. The endtime can be used to determine that it is an old candle
        /// </summary>
        /// <param name="instruments"></param>
        /// <param name="callbackAction"></param>
        /// <returns></returns>
        SubscriptionInfo SubscribeOHLC(string[] instruments, Action<OHLCPayload> callbackAction);

        /// <summary>
        /// Trade feed for a currency pair.
        /// </summary>
        /// <param name="instruments"></param>
        /// <param name="callbackAction"></param>
        /// <returns></returns>
        SubscriptionInfo SubscribeTrade(string[] instruments, Action<TradePayload> callbackAction);

        /// <summary>
        /// Spread feed for a currency pair.
        /// </summary>
        /// <param name="instruments"></param>
        /// <param name="callbackAction"></param>
        /// <returns></returns>
        SubscriptionInfo SubscribeSpread(string[] instruments, Action<SpreadPayload> callbackAction);

        /// <summary>
        /// Order book levels. On subscription, a snapshot will be published at the specified depth, following the snapshot, level updates will be published
        /// </summary>
        /// <param name="instruments"></param>
        /// <param name="callbackAction"></param>
        /// <returns></returns>
        SubscriptionInfo SubscribeBook(string[] instruments, Action<OrderBookPayload> callbackAction);

        /// <summary>
        /// Отписаться от рассылки
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        bool Unsubscribe(long subscriptionId);
    }
}
