using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData
{
    /// <summary>
    /// 
    /// </summary>
    public class TickerInformation
    {
        public object[] error { get; set; }
        public Dictionary<string, TickerInfo> result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TickerInfo
    {
        /// <summary>
        /// asks
        /// </summary>
        [JsonProperty("a")]
        public string[] a { get; set; }

        /// <summary>
        /// asks
        /// </summary>
        [JsonProperty("b")]
        public string[] b { get; set; }

        /// <summary>
        /// asks
        /// </summary>
        [JsonProperty("c")]
        public string[] c { get; set; }

        /// <summary>
        /// asks
        /// </summary>
        [JsonProperty("v")]
        public string[] v { get; set; }

        /// <summary>
        /// asks
        /// </summary>
        [JsonProperty("p")]
        public string[] p { get; set; }

        /// <summary>
        /// asks
        /// </summary>
        [JsonProperty("t")]
        public int[] t { get; set; }

        /// <summary>
        /// asks
        /// </summary>
        [JsonProperty("l")]
        public string[] l { get; set; }

        /// <summary>
        /// asks
        /// </summary>
        [JsonProperty("h")]
        public string[] h { get; set; }

        /// <summary>
        /// asks
        /// </summary>
        [JsonProperty("o")]
        public string o { get; set; }
    }


}
