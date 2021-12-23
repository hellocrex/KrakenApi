using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.PublicWebSocket
{
    /// <summary>
    /// Response on subscribe to a topic ticker.
    /// </summary>
    public class SpreadPayload
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("channelID")]
        public int ChannelID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public Dictionary <int, SpreadInfo[]> SpreadInfo { get; set; }
        public SpreadInfo[] SpreadInfo { get; set; }

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

    public class SpreadInfo
    {
        /// <summary>
        /// Bid price
        /// </summary>
        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        /// <summary>
        /// Ask price
        /// </summary>
        [JsonProperty("ask")]
        public decimal Ask { get; set; }

        /// <summary>
        /// Time, seconds since epoch
        /// </summary>
        [JsonProperty("timestamp")]
        public decimal Timestamp { get; set; }

        /// <summary>
        /// Bid Volume
        /// </summary>
        [JsonProperty("bidVolume")]
        public decimal BidVolume { get; set; }

        /// <summary>
        /// Ask Volume
        /// </summary>
        [JsonProperty("askVolume")]
        public decimal AskVolume { get; set; }
    }

}
