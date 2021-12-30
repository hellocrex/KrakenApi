using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding
{
    /// <summary>
    /// Retrieve information about recent deposits and withdrawals made.
    /// </summary>
    public class StatusOfRecent
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }
        
        [JsonProperty("result")]
        public RecentDepositsResult[] Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RecentDepositsResult
    {
        /// <summary>
        /// Name of deposit method
        /// </summary>
        [JsonProperty("method")]
        public string MethodName { get; set; }

        /// <summary>
        /// Asset class
        /// </summary>
        [JsonProperty("aclass")]
        public string AssetClass { get; set; }

        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("asset")]
        public string Asset { get; set; }

        /// <summary>
        /// Reference ID
        /// </summary>
        [JsonProperty("refid")]
        public string RefId { get; set; }

        /// <summary>
        /// Method transaction ID
        /// </summary>
        [JsonProperty("txid")]
        public string TxId { get; set; }

        /// <summary>
        /// Method transaction information
        /// </summary>
        [JsonProperty("info")]
        public string Info { get; set; }

        /// <summary>
        /// Amount deposited
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Amount deposited
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Unix timestamp when request was made
        /// </summary>
        [JsonProperty("time")]
        public int Time { get; set; }

        /// <summary>
        /// Status of deposit
        /// </summary>
        [JsonProperty("status")]
        public DepositAndWithdrawStatus Status { get; set; }

        /// <summary>
        /// Addition status properties 
        /// </summary>
        [JsonProperty("status-prop", NullValueHandling = NullValueHandling.Ignore)]
        public StatusProp? StatusProp { get; set; }
    }

}
