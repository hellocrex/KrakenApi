using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.PublicWebSocket
{
    /// <summary>
    /// Response on subscribe to a topic ticker.
    /// </summary>
    public class TickerPayload
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("channelID")]
        public int ChannelID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TickerInfo TickerInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("channelName")]
        public string channelName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }
    }


    public class TickerInfo
    {
        /// <summary>
        /// Ask
        /// </summary>
        [JsonProperty("a")]
        public object[] Ask { get; set; }

        /// <summary>
        /// Bid
        /// </summary>
        [JsonProperty("b")]
        public object[] Bid { get; set; }

        /// <summary>
        /// Close
        /// </summary>
        [JsonProperty("c")]
        public string[] Close { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        [JsonProperty("v")]
        public string[] Volume { get; set; }

        /// <summary>
        /// Volume weighted average price
        /// </summary>
        [JsonProperty("p")]
        public string[] VolumePrice { get; set; }

        /// <summary>
        /// Number of trades
        /// </summary>
        [JsonProperty("t")]
        public int[] NumberTrades { get; set; }

        /// <summary>
        /// Low price
        /// </summary>
        [JsonProperty("l")]
        public string[] LowPrice { get; set; }

        /// <summary>
        /// High price
        /// </summary>
        [JsonProperty("h")]
        public string[] HighPrice { get; set; }

        /// <summary>
        /// Open Price
        /// </summary>
        [JsonProperty("o")]
        public string[] OpenPrice { get; set; }
    }

}
