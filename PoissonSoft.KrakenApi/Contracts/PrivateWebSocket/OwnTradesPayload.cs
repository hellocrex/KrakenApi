using System.Collections.Generic;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.PrivateWebSocket
{
    /// <summary>
    /// Own trades. On subscription last 50 trades for the user will be sent, followed by new trades.
    /// </summary>
    public class OwnTradesPayload
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, OwnTradeInfo> OwnTradeInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("channelName", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ChannelName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("sequence")]
        public int? Sequence { get; set; }
    }

    public class OwnTradeInfo
    {
        /// <summary>
        /// order responsible for execution of trade
        /// </summary>
        [JsonProperty("ordertxid")]
        public string OrderTxId { get; set; }

        /// <summary>
        /// Position trade id
        /// </summary>
        [JsonProperty("postxid")]
        public string PosTxId { get; set; }

        /// <summary>
        /// Asset pair
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }

        /// <summary>
        /// unix timestamp of trade
        /// </summary>
        [JsonProperty("time")]
        public decimal Time { get; set; }

        /// <summary>
        /// type of order (buy/sell)
        /// </summary>
        [JsonProperty("type")]
        public OrderSide Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("orderType")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// average price order was executed at (quote currency)
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// total cost of order (quote currency)
        /// </summary>
        [JsonProperty("cost")]
        public decimal Cost { get; set; }

        /// <summary>
        /// total fee (quote currency)
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// volume (base currency)
        /// </summary>
        [JsonProperty("vol")]
        public decimal Volume { get; set; }

        /// <summary>
        /// initial margin (quote currency)
        /// </summary>
        [JsonProperty("margin")]
        public decimal? Margin { get; set; }

        /// <summary>
        /// user reference ID
        /// </summary>
        [JsonProperty("userref")]
        public int? Userref { get; set; }
    }
}
