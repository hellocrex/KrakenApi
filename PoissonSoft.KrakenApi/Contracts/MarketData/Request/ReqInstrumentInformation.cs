using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.MarketData.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ReqInstrumentInformation
    {
        /// <summary>
        /// Filled side. The filled side is set to the taker by default
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Instrument { get; set; }

        /// <summary>
        /// Info to retrieve. (optional)
        /// </summary>
        [JsonProperty("info", NullValueHandling = NullValueHandling.Ignore)]
        public RetrieveInfo? Info { get; set; }
    }
}
