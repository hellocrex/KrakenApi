using System.Collections.Generic;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;
using PoissonSoft.KrakenApi.Contracts.MarketData;

namespace PoissonSoft.KrakenApi.Contracts.PublicWebSocket
{
    /// <summary>
    /// Response on subscribe to a topic ticker.
    /// </summary>
    public class TradePayload
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("channelID")]
        public int ChannelID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TradeInfo[][] TradeInfo { get; set; }

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


    public class TradeInfo
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

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("side")]
        public OrderSide OrderSide { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("orderType")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("misc")]
        public string Misc { get; set; }
    }
}
