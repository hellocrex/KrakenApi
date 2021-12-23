namespace PoissonSoft.KrakenApi.MarketDataStreams
{
    public static class SubscribeMethodName
    {
        // public
        public const string Book = "book";
        public const string Ohlc = "ohlc";
        public const string Spread = "spread";
        public const string Ticker = "ticker";
        public const string Trade = "trade";

        // private
        public const string OwnTrades = "ownTrades";
        public const string OpenOrders = "openOrders";
        public const string AddOrder = "addOrder";
        public const string CancelOrder = "cancelOrder";
        public const string CancelAll = "cancelAll";
        public const string CancelAllOrdersAfter = "cancelAllOrdersAfter";

    }
}
