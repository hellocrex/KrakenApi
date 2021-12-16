using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    /// <summary>
    /// 
    /// </summary>
    public class TradeVolume
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public TradeVolumeResult Result { get; set; }
    }

    public class TradeVolumeResult
    {
        /// <summary>
        /// Volume currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Current discount volume
        /// </summary>
        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        /// <summary>
        /// Fee Tier Info
        /// </summary>
        [JsonProperty("fees")]
        public Dictionary<string, FeesInfo> Fees { get; set; }

        /// <summary>
        /// Fee Tier Info
        /// </summary>
        [JsonProperty("fees_maker")]
        public Dictionary<string, FeesInfo> FeesMaker { get; set; }
    }
}
