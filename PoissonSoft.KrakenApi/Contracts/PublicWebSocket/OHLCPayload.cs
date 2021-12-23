using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.PublicWebSocket
{
    /// <summary>
    /// Response on subscribe to a topic ticker.
    /// </summary>
    public class OHLCPayload
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("channelID")]
        public int ChannelID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OHLCData[] OHLCData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pair", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Instrument { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("channelName", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ChannelName { get; set; }
    }

    public class OHLCData
    {
        /// <summary>
        /// Begin time of interval, in seconds since epoch
        /// </summary>
        [JsonProperty("time")]
        public decimal Time { get; set; }

        /// <summary>
        /// End time of interval, in seconds since epoch
        /// </summary>
        [JsonProperty("etime")]
        public decimal Etime { get; set; }

        /// <summary>
        /// Open price of interval
        /// </summary>
        [JsonProperty("open")]
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// High price within interval
        /// </summary>
        [JsonProperty("high")]
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Low price within interval
        /// </summary>
        [JsonProperty("low")]
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Close price of interval
        /// </summary>
        [JsonProperty("close")]
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// Volume weighted average price within interval
        /// </summary>
        [JsonProperty("vwap")]
        public decimal VolumeWAP { get; set; }

        /// <summary>
        /// Accumulated volume within interval
        /// </summary>
        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        /// <summary>
        /// Number of trades within interval
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
