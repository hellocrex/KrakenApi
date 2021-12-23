using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.PrivateWebSocket
{
    /// <summary>
    /// 
    /// </summary>
    public class AddOrderPayload
    {
        /// <summary>
        /// addOrder
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }

        /// <summary>
        /// Session token string
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Optional - client originated requestID sent as acknowledgment in the message response
        /// </summary>
        [JsonProperty("reqid", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReqId { get; set; }

        /// <summary>
        /// Order type - market|limit|stop-loss|take-profit|stop-loss-limit|take-profit-limit|settle-position
        /// </summary>
        [JsonProperty("ordertype")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Side, buy or sell
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }

        /// <summary>
        /// type of order (buy/sell)
        /// </summary>
        [JsonProperty("type")]
        public OrderSide Type { get; set; }

        /// <summary>
        /// Optional dependent on order type - order price
        /// </summary>
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        /// <summary>
        /// Optional dependent on order type - order secondary price
        /// </summary>
        [JsonProperty("price2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price2 { get; set; }

        /// <summary>
        /// volume (base currency)
        /// </summary>
        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        /// <summary>
        /// amount of leverage desired (optional; default = none)
        /// </summary>
        [JsonProperty("leverage", NullValueHandling = NullValueHandling.Ignore)]
        public int? Leverage { get; set; }

        /// <summary>
        /// Optional - comma delimited list of order flags. viqc = volume in quote currency (not currently available), fcib = prefer fee in base currency,
        /// fciq = prefer fee in quote currency, nompp = no market price protection, post = post only order (available when ordertype = limit)
        /// </summary>
        [JsonProperty("oflags", NullValueHandling = NullValueHandling.Ignore)]
        public string Oflags { get; set; }

        /// <summary>
        /// Optional - scheduled start time. 0 = now (default) +<n> = schedule start time <n> seconds from now <n> = unix timestamp of start time
        /// </summary>
        [JsonProperty("starttm", NullValueHandling = NullValueHandling.Ignore)]
        public string Starttime { get; set; }

        /// <summary>
        /// Optional - expiration time. 0 = no expiration (default) +<n> = expire <n> seconds from now <n> = unix timestamp of expiration time
        /// </summary>
        [JsonProperty("expiretm", NullValueHandling = NullValueHandling.Ignore)]
        public string ExpireTime { get; set; }

        /// <summary>
        /// Optional - RFC3339 timestamp (e.g. 2021-04-01T00:18:45Z) after which matching engine should reject new order request,
        /// in presence of latency or order queueing. min now() + 5 seconds, max now() + 90 seconds. Defaults to 90 seconds if not specified.
        /// </summary>
        [JsonProperty("deadline", NullValueHandling = NullValueHandling.Ignore)]
        public string DeadLine { get; set; }

        /// <summary>
        /// Optional - user reference ID (should be an integer in quotes)
        /// </summary>
        [JsonProperty("userref", NullValueHandling = NullValueHandling.Ignore)]
        public string Userref { get; set; }

        /// <summary>
        /// Optional - validate inputs only; do not submit order
        /// </summary>
        [JsonProperty("validate", NullValueHandling = NullValueHandling.Ignore)]
        public string Validate { get; set; }

        /// <summary>
        /// Optional - close order type.
        /// </summary>
        [JsonProperty("close[ordertype]", NullValueHandling = NullValueHandling.Ignore)]
        public string closeOrderType { get; set; }

        /// <summary>
        /// Optional - close order price.
        /// </summary>
        [JsonProperty("close[price]", NullValueHandling = NullValueHandling.Ignore)]
        public string ClosePrice { get; set; }

        /// <summary>
        /// Optional - close order secondary price.
        /// </summary>
        [JsonProperty("close[price2]", NullValueHandling = NullValueHandling.Ignore)]
        public string ClosePrice2 { get; set; }

        /// <summary>
        /// Optional - time in force. Supported values include GTC (good-til-cancelled; default),
        /// IOC (immediate-or-cancel), GTD (good-til-date; expiretm must be specified).
        /// </summary>
        [JsonProperty("timeinforce")]
        public TimeInForce TimeInForce { get; set; }
    }
}
