using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;
using PoissonSoft.KrakenApi.Contracts.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.MarketData.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class ReqOHLCData
    {
        /// <summary>
        /// Asset pair to get data for
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }

        /// <summary>
        /// Time frame interval in minutes
        /// </summary>
        [JsonProperty("interval", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumExConverter), TimeInterval.Unknown)]
        public TimeInterval? Interval { get; set; }

        /// <summary>
        /// Return committed OHLC data since given ID
        /// </summary>
        [JsonProperty("since", NullValueHandling = NullValueHandling.Ignore)]
        public int? StartTimeFrom { get; set; }
    }
}
