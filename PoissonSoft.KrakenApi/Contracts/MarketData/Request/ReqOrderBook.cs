using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData.Request
{
    public class ReqOrderBook
    {
        /// <summary>
        /// Asset pair to get data for
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }

        /// <summary>
        /// maximum number of asks/bids
        /// Default: 100
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int? Count { get; set; }
    }
}
