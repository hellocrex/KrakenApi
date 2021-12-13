using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    /// <summary>
    /// Retrieve a summary of collateral balances, margin position valuations, equity and margin level.
    /// </summary>
    public class OpenOrders
    {
        [JsonProperty("error")]
        public object[] error { get; set; }
        
        [JsonProperty("result")]
        public OpenOrdersInfo Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OpenOrdersInfo
    {
        /// <summary>
        /// Open Order
        /// </summary>
        [JsonProperty("open")]
        public Dictionary<string, OrdersInfo> Open { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OrdersInfo
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
        public int OpenTime { get; set; }

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
    }

    /// <summary>
    /// Order description info
    /// </summary>
    public class OrderDescription
    {
        /// <summary>
        /// Asset pair
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }

        /// <summary>
        /// Type of order (buy/sell)
        /// </summary>
        [JsonProperty("type")]
        public OrderSide OrderSide { get; set; }

        /// <summary>
        /// Order type
        /// </summary>
        [JsonProperty("ordertype")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// primary price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Secondary price
        /// </summary>
        [JsonProperty("price2")]
        public decimal Price2 { get; set; }

        /// <summary>
        /// Amount of leverage
        /// </summary>
        [JsonProperty("leverage")]
        public string Leverage { get; set; }

        /// <summary>
        /// Order description
        /// </summary>
        [JsonProperty("order")]
        public string OrderDescr { get; set; }

        /// <summary>
        /// Conditional close order description (if conditional close set)
        /// </summary>
        [JsonProperty("close")]
        public string Close { get; set; }
    }

}
