using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData
{
    /// <summary>
    /// 
    /// </summary>
    public class TickerInformation
    {
        [JsonProperty("result")]
        public Dictionary<string, TickerInfo> Result { get; set; }

        [JsonProperty("error")]
        public string[] Error { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TickerInfo
    {
        /// <summary>
        /// Asks
        /// </summary>
        [JsonProperty("a")]
        public string[] Asks { get; set; }

        /// <summary>
        /// Bid
        /// </summary>
        [JsonProperty("b")]
        public string[] Bid { get; set; }

        /// <summary>
        /// Last trade closed
        /// </summary>
        [JsonProperty("c")]
        public string[] Closed { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        [JsonProperty("v")]
        public string[] Volume { get; set; }

        /// <summary>
        /// Volume weighted average price
        /// </summary>
        [JsonProperty("p")]
        public string[] VolumeAveragePrice { get; set; }

        /// <summary>
        /// Number of trades
        /// </summary>
        [JsonProperty("t")]
        public int[] TradeCount { get; set; }

        /// <summary>
        /// Low
        /// </summary>
        [JsonProperty("l")]
        public string[] Low { get; set; }

        /// <summary>
        /// High
        /// </summary>
        [JsonProperty("h")]
        public string[] High { get; set; }

        /// <summary>
        /// Today's opening price
        /// </summary>
        [JsonProperty("o")]
        public string OpenPrice { get; set; }
    }
}
