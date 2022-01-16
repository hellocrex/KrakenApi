using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    public class GeneralOrderInfo
    {   
        /// <summary>
        /// Referral order transaction ID that created this order
        /// </summary>
        [JsonProperty("refid")]
        public string Refid { get; set; }

        /// <summary>
        /// User reference id
        /// </summary>
        [JsonProperty("userref")]
        public string Userref { get; set; }

        /// <summary>
        /// Status of order
        /// </summary>
        [JsonProperty("status")]
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Unix timestamp of when order was placed
        /// </summary>
        [JsonProperty("opentm")]
        public decimal OpenTime { get; set; }

        /// <summary>
        /// Unix timestamp of order start time (or 0 if not set)
        /// </summary>
        [JsonProperty("starttm")]
        public int StartTime { get; set; }

        /// <summary>
        /// Unix timestamp of order end time (or 0 if not set)
        /// </summary>
        [JsonProperty("expiretm")]
        public int ExpireTime { get; set; }

        /// <summary>
        /// Order description info
        /// </summary>
        [JsonProperty("descr")]
        public OrderDescription Description { get; set; }

        /// <summary>
        /// Volume of order (base currency)
        /// </summary>
        [JsonProperty("vol")]
        public decimal Volume { get; set; }

        /// <summary>
        /// Volume executed (base currency)
        /// </summary>
        [JsonProperty("vol_exec")]
        public decimal VolExecute { get; set; }

        /// <summary>
        /// Total cost (quote currency unless)
        /// </summary>
        [JsonProperty("cost")]
        public decimal Cost { get; set; }

        /// <summary>
        /// Total fee (quote currency)
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Average price (quote currency)
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Stop price (quote currency)
        /// </summary>
        [JsonProperty("stopprice")]
        public decimal StopPrice { get; set; }

        /// <summary>
        /// Triggered limit price (quote currency, when limit based order type triggered)
        /// </summary>
        [JsonProperty("limitprice")]
        public decimal LimitPrice { get; set; }

        /// <summary>
        /// Comma delimited list of miscellaneous info
        /// </summary>
        [JsonProperty("misc")]
        public string Misc { get; set; }

        /// <summary>
        /// Comma delimited list of order flags
        /// </summary>
        [JsonProperty("oflags")]
        public string Oflags { get; set; }

        /// <summary>
        /// List of trade IDs related to order (if trades info requested and data available)
        /// </summary>
        [JsonProperty("trades")]
        public string[] Trades { get; set; }

        /// <summary>
        /// Unix timestamp of when order was closed
        /// </summary>
        [JsonProperty("closetm")]
        public decimal CloseTime { get; set; }

        /// <summary>
        /// Additional info on status (if any)
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

    }
}
