using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
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
