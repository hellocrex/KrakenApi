using System;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserTrading.Request
{
    /// <summary>
    /// Place a new order.
    /// </summary>
    public class ReqNewOrder
    {
        /// <summary>
        /// Nonce used in construction of API-Sign header
        /// </summary>
        [JsonProperty("nonce")]
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        /// <summary>
        /// User reference id
        /// </summary>
        [JsonProperty("userref", NullValueHandling = NullValueHandling.Ignore)]
        public string UserRef { get; set; }

        /// <summary>
        /// User reference id
        /// </summary>
        [JsonProperty("ordertype")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Order direction (buy/sell)
        /// </summary>
        [JsonProperty("type")]
        public OrderSide OrderSide { get; set; }

        /// <summary>
        /// Order quantity in terms of the base asset
        /// </summary>
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Volume { get; set; }

        /// <summary>
        /// Asset pair id or altname
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        /// <summary>
        /// Secondary Price
        /// </summary>
        [JsonProperty("price2", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price2 { get; set; }

        /// <summary>
        /// Amount of leverage desired (default = none)
        /// </summary>
        [JsonProperty("leverage", NullValueHandling = NullValueHandling.Ignore)]
        public string Leverage { get; set; }

        /// <summary>
        /// Comma delimited list of order flags
        /// </summary>
        [JsonProperty("oflags", NullValueHandling = NullValueHandling.Ignore)]
        public string Oflags { get; set; }

        /// <summary>
        /// Time-in-force of the order to specify how long it should remain in the order book before being cancelled.
        /// </summary>
        [JsonProperty("timeinforce", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInForce? TimeInForce { get; set; }

        /// <summary>
        /// Scheduled start time. Can be specified as an absolute timestamp or as a number of seconds in the future
        /// </summary>
        [JsonProperty("starttm", NullValueHandling = NullValueHandling.Ignore)]
        public string StartTime { get; set; }

        /// <summary>
        /// Expiration time
        /// 0 no expiration (default)
        /// +<n> = expire seconds from now, minimum 5 seconds
        /// <n> = unix timestamp of expiration time
        /// </summary>
        [JsonProperty("expiretm", NullValueHandling = NullValueHandling.Ignore)]
        public string ExpireTime { get; set; }

        /// <summary>
        /// Conditional close order type.
        /// </summary>
        [JsonProperty("close[ordertype]", NullValueHandling = NullValueHandling.Ignore)]
        public CloseOrderStatus? CloseOrderStatus { get; set; }

        /// <summary>
        /// Conditional close order price
        /// </summary>
        [JsonProperty("close[price]", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ClosePrice { get; set; }

        /// <summary>
        /// Conditional close order price
        /// </summary>
        [JsonProperty("close[price2]", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ClosePrice2 { get; set; }

        /// <summary>
        /// RFC3339 timestamp (e.g. 2021-04-01T00:18:45Z) after which the matching engine should reject the new order request,
        /// in presence of latency or order queueing. min now() + 5 seconds, max now() + 60 seconds
        /// </summary>
        [JsonProperty("deadline", NullValueHandling = NullValueHandling.Ignore)]
        public string DeadLine { get; set; }

        /// <summary>
        /// Validate inputs only. Do not submit order.
        /// </summary>
        [JsonProperty("validate", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Validate { get; set; }
    }
}
