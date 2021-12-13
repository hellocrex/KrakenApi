using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ReqTickerInformation
    {
        /// <summary>
        /// Filled side. The filled side is set to the taker by default
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }
    }
}
