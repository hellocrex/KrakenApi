using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ReqAssetInfo
    {
        /// <summary>
        /// Comma delimited list of assets to get info on.
        /// </summary>
        [JsonProperty("asset", NullValueHandling = NullValueHandling.Ignore)]
        public string Ticker { get; set; }

        /// <summary>
        /// Asset class. (optional, default: currency)
        /// </summary>
        [JsonProperty("aclass", NullValueHandling = NullValueHandling.Ignore)]
        public string AssetClass { get; set; }
    }
}
