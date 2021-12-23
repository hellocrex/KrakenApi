using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.PublicWebSocket
{
    /// <summary>
    /// Response on subscribe to a topic ticker.
    /// </summary>
    public class OrderBookPayload
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("channelID")]
        public int ChannelID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("orderBook")]
        //public Dictionary<string, decimal[][]>[] Orderbook { get; set; }
        public OrkerBook[] Orderbook { get; set; }

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

    public class OrkerBook
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ask")]
        public OrderAsk[] Ask { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("bid")]
        public OrderAsk[] Bid { get; set; }
    }

    public class OrderAsk
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("time")]
        public decimal Time { get; set; }

    }

}
