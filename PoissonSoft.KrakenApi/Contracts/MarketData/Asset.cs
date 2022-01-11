using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData
{
    /// <summary>
    /// 
    /// </summary>
    public class Asset
    {
        [JsonProperty("result")]
        public Dictionary<string, AssetInfo> Result { get; set; }

        [JsonProperty("error")]
        public string[] Error { get; set; }
    }

    /// <summary>
    /// Asset Info
    /// </summary>
    public class AssetInfo
    {
        /// <summary>
        /// Asset Class
        /// </summary>
        [JsonProperty("aclass")]
        public string AssetClass { get; set; }

        /// <summary>
        /// Alternate name
        /// </summary>
        [JsonProperty("altname")]
        public string AltName { get; set; }

        /// <summary>
        /// Scaling decimal places for record keeping
        /// </summary>
        [JsonProperty("decimals")]
        public string decimals { get; set; }

        /// <summary>
        /// Scaling decimal places for output display
        /// </summary>
        [JsonProperty("display_decimals")]
        public string display_decimals { get; set; }
    }
}
