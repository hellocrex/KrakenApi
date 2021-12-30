using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding
{
    /// <summary>
    /// Retrieve fee information about potential withdrawals for a particular asset, key and amount.
    /// </summary>
    public class WithdrawalInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }
        
        [JsonProperty("result")]
        public WithdrawalInfoResult Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class WithdrawalInfoResult
    {
        /// <summary>
        /// Name of deposit method
        /// </summary>
        [JsonProperty("method")]
        public string MethodName { get; set; }

        /// <summary>
        /// Maximum net amount that can be withdrawn right now
        /// </summary>
        [JsonProperty("limit")]
        public decimal Limit { get; set; }

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
    }

}
