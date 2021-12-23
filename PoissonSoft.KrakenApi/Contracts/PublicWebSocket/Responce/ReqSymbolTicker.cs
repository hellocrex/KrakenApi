using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.PublicWebSocket.Responce
{
    /// <summary>
    /// Request. Subscribe to a topic on a single or multiple currency pairs.
    /// </summary>
    public class SymbolTicker
    {
        /// <summary>
        /// subscribe
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }

        /// <summary>
        /// Optional - client originated ID reflected in response message
        /// </summary>
        [JsonProperty("reqid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long ReqId { get; set; }

        /// <summary>
        /// Optional - Array of currency pairs. Format of each pair is "A/B",
        /// where A and B are ISO 4217-A3 for standardized assets and popular unique symbol if not standardized.
        /// </summary>
        [JsonProperty("pair", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Instruments { get; set; }

        [JsonProperty("subscription")]
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
    }
}
