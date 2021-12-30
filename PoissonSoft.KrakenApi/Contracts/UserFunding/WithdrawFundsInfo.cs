using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding
{
    /// <summary>
    /// 
    /// </summary>
    public class WithdrawFundsInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }
        
        [JsonProperty("result")]
        public WithdrawFundsResult Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class WithdrawFundsResult
    {
        /// <summary>
        /// Reference ID
        /// </summary>
        [JsonProperty("refid")]
        public string RefId { get; set; }
    }
}
