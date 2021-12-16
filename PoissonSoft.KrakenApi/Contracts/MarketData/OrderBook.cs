using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData
{
    public class OrderBook
    {
        public string[] Error { get; set; }
        public Dictionary<string, OrderBookInfo> Result { get; set; }
    }

    /// <summary>
    /// Asset Pair Order Book Entries
    /// </summary>
    public class OrderBookInfo
    {
        /// <summary>
        /// Asks
        /// </summary>
        [JsonProperty("asks")]
        public decimal[][] Asks { get; set; }

        /// <summary>
        /// Bids
        /// </summary>
        [JsonProperty("bids")]
        public decimal[][] Bids { get; set; }
    }
}
