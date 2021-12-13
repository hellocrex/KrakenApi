using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    /// <summary>
    /// Retrieve a summary of collateral balances, margin position valuations, equity and margin level.
    /// </summary>
    public class TradeBalance
    {
        [JsonProperty("error")]
        public object[] error { get; set; }
        
        [JsonProperty("result")]
        public TradeBalanceInfo Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TradeBalanceInfo
    {
        /// <summary>
        /// Equivalent balance (combined balance of all currencies)
        /// </summary>
        [JsonProperty("eb")]
        public string EquivalentBalance { get; set; }

        /// <summary>
        /// Trade balance (combined balance of all equity currencies)
        /// </summary>
        [JsonProperty("tb")]
        public string TradeBalance { get; set; }

        /// <summary>
        /// Margin amount of open positions
        /// </summary>
        [JsonProperty("m")]
        public string MarginAmountOpenPositions { get; set; }

        /// <summary>
        /// Unrealized net profit/loss of open positions
        /// </summary>
        [JsonProperty("n")]
        public string UnrealizedProfit { get; set; }

        /// <summary>
        /// Cost basis of open positions
        /// </summary>
        [JsonProperty("c")]
        public string CostBasisOpenPositions { get; set; }

        /// <summary>
        /// Current floating valuation of open positions
        /// </summary>
        [JsonProperty("v")]
        public string CurrentFloatingValuationOpenPositions { get; set; }

        /// <summary>
        /// Equity: trade balance + unrealized net profit/loss
        /// </summary>
        [JsonProperty("e")]
        public string Equity { get; set; }

        /// <summary>
        /// Free margin: Equity - initial margin (maximum margin available to open new positions)
        /// </summary>
        [JsonProperty("mf")]
        public string FreeMargin { get; set; }

        /// <summary>
        /// Margin level: (equity / initial margin) * 100
        /// </summary>
        [JsonProperty("ml")]
        public string MarginLevel { get; set; }
    }

}
