using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;
using PoissonSoft.KrakenApi.Contracts.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.MarketDataStream
{

    /// <summary>
    /// Request to manage Market Data Streams
    /// </summary>
    public class CommandRequest
    {
        /// <summary>
        /// subscribe
        /// </summary>
        [JsonProperty("event")]
        [JsonConverter(typeof(StringEnumExConverter), CommandRequestMethod.Unknown)]
        public CommandRequestMethod Event { get; set; }

        /// <summary>
        /// Optional - client originated ID reflected in response message
        /// </summary>
        [JsonProperty("reqid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long RequestId { get; set; }

        /// <summary>
        /// Session token string
        /// </summary>
        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        /// <summary>
        /// Optional - Array of currency pairs. Format of each pair is "A/B",
        /// where A and B are ISO 4217-A3 for standardized assets and popular unique symbol if not standardized.
        /// </summary>
        [JsonProperty("pair", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Instruments { get; set; }

        /// <summary>
        /// Order type - market|limit|stop-loss|take-profit|stop-loss-limit|take-profit-limit|settle-position
        /// </summary>
        [JsonProperty("ordertype", NullValueHandling = NullValueHandling.Ignore)]
        public OrderType? OrderType { get; set; }

        /// <summary>
        /// type of order (buy/sell)
        /// </summary>
        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public OrderSide? OrderSide { get; set; }

        /// <summary>
        /// Optional dependent on order type - order price
        /// </summary>
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        /// <summary>
        /// volume (base currency)
        /// </summary>
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Volume { get; set; }

        /// <summary>
        /// Optional - time in force. Supported values include GTC (good-til-cancelled; default),
        /// IOC (immediate-or-cancel), GTD (good-til-date; expiretm must be specified).
        /// </summary>
        [JsonProperty("timeinforce", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInForce? TimeInForce { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("subscription", NullValueHandling = NullValueHandling.Ignore)]
        public SubscriptionContext Subscription { get; set; }
    }

    public class SubscriptionContext
    {
        /// <summary>
        /// Optional - depth associated with book subscription in number of levels each side, default 10. Valid Options are: 10, 25, 100, 500, 1000
        /// </summary>
        [JsonProperty("depth", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Depth { get; set; }

        /// <summary>
        /// book|ohlc|openOrders|ownTrades|spread|ticker|trade|*, * for all available channels depending on the connected environment
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
    }
}
