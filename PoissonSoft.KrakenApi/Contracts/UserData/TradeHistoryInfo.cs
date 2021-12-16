using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    public class TradeHistoryInfo
    {
        /// <summary>
        /// Order responsible for execution of trade
        /// </summary>
        [JsonProperty("ordertxid")]
        public string OrderTxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("postxid")]
        public string PosTxId { get; set; }

        /// <summary>
        /// Asset pair
        /// </summary>
        [JsonProperty("pair")]
        public string Instrument { get; set; }

        /// <summary>
        /// Unix timestamp of trade
        /// </summary>
        [JsonProperty("time")]
        public string Time { get; set; }

        /// <summary>
        /// Type of order (buy/sell)
        /// </summary>
        [JsonProperty("type")]
        public OrderSide Type { get; set; }

        /// <summary>
        /// Order type
        /// </summary>
        [JsonProperty("ordertype")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// Average price order was executed at (quote currency)
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Total cost of order (quote currency)
        /// </summary>
        [JsonProperty("cost")]
        public decimal Cost { get; set; }

        /// <summary>
        /// Total fee (quote currency)
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Volume of order (base currency)
        /// </summary>
        [JsonProperty("vol")]
        public decimal Volume { get; set; }

        /// <summary>
        /// Initial margin (quote currency)
        /// </summary>
        [JsonProperty("margin")]
        public decimal Margin { get; set; }

        /// <summary>
        /// Comma delimited list of miscellaneous info
        /// </summary>
        [JsonProperty("misc")]
        public string Misc { get; set; }

        /// <summary>
        /// Position status (open/closed)
        /// </summary>
        [JsonProperty("posstatus")]
        public string PosStatus { get; set; }

        /// <summary>
        /// Average price of closed portion of position (quote currency)
        /// </summary>
        [JsonProperty("cprice")]
        public object AvgClosePrice { get; set; }

        /// <summary>
        /// Total cost of closed portion of position (quote currency)
        /// </summary>
        [JsonProperty("ccost")]
        public object CloseCost { get; set; }

        /// <summary>
        /// Total fee of closed portion of position (quote currency)
        /// </summary>
        [JsonProperty("cfee")]
        public object CloseFee { get; set; }

        /// <summary>
        /// Total fee of closed portion of position (quote currency)
        /// </summary>
        [JsonProperty("cvol")]
        public object CloseVol { get; set; }

        /// <summary>
        /// Total margin freed in closed portion of position (quote currency)
        /// </summary>
        [JsonProperty("cmargin")]
        public object CloseMargin { get; set; }

        /// <summary>
        /// Net profit/loss of closed portion of position (quote currency, quote currency scale)
        /// </summary>
        [JsonProperty("net")]
        public object NetProfit { get; set; }

        /// <summary>
        /// List of closing trades for position (if available)
        /// </summary>
        [JsonProperty("trades")]
        public string[] Trades { get; set; }

    }
}
