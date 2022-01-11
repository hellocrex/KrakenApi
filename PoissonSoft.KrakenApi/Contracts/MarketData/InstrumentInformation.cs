using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData
{
    /// <summary>
    /// 
    /// </summary>
    public class InstrumentInformation
    {
        [JsonProperty("result")]
        public Dictionary<string, InstrumentInfo> Result { get; set; }

        [JsonProperty("error")]
        public string[] Error { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InstrumentInfo
    {
        /// <summary>
        /// Alternate pair name
        /// </summary>
        [JsonProperty("altname")]
        public string AltName { get; set; }

        /// <summary>
        /// WebSocket pair name (if available)
        /// </summary>
        [JsonProperty("wsname")]
        public string WsName { get; set; }

        /// <summary>
        /// Asset class of base component
        /// </summary>
        [JsonProperty("aclass_base")]
        public string AclassBase { get; set; }

        /// <summary>
        /// Asset ID of base component
        /// </summary>
        [JsonProperty("base")]
        public string Base { get; set; }

        /// <summary>
        /// Asset class of quote component
        /// </summary>
        [JsonProperty("aclass_quote")]
        public string AclassQuote { get; set; }

        /// <summary>
        /// Asset ID of quote component
        /// </summary>
        [JsonProperty("quote")]
        public string Quote { get; set; }

        /// <summary>
        /// Scaling decimal places for pair
        /// </summary>
        [JsonProperty("pair_decimals")]
        public int PairDecimals { get; set; }

        /// <summary>
        /// Scaling decimal places for volume
        /// </summary>
        [JsonProperty("lot_decimals")]
        public int LotDecimals { get; set; }

        /// <summary>
        /// Amount to multiply lot volume by to get currency volume
        /// </summary>
        [JsonProperty("lot_multiplier")]
        public int LotMultiplier { get; set; }

        /// <summary>
        /// Array of leverage amounts available when buying
        /// </summary>
        [JsonProperty("leverage_buy")]
        public int[] LeverageBuy { get; set; }

        /// <summary>
        /// Array of leverage amounts available when selling
        /// </summary>
        [JsonProperty("leverage_sell")]
        public int[] LeverageSell { get; set; }

        /// <summary>
        /// Fee schedule array in [<volume>, <percent fee>] tuples
        /// </summary>
        [JsonProperty("fees")]
        public decimal[][] Fees { get; set; }

        /// <summary>
        /// Maker fee schedule array in [<volume>, <percent fee>] tuples (if on maker/taker)
        /// </summary>
        [JsonProperty("fees_maker")]
        public decimal[][] FeesMaker { get; set; }

        /// <summary>
        /// Volume discount currency
        /// </summary>
        [JsonProperty("fee_volume_currency")]
        public string FeeVolumeCurrency { get; set; }

        /// <summary>
        /// Margin call level
        /// </summary>
        [JsonProperty("margin_call")]
        public int MarginCall { get; set; }

        /// <summary>
        /// Stop-out/liquidation margin level
        /// </summary>
        [JsonProperty("margin_stop")]
        public int MarginStop { get; set; }

        /// <summary>
        /// Minimum order size (in terms of base currency)
        /// </summary>
        [JsonProperty("ordermin")]
        public decimal Ordermin { get; set; }
    }
}
