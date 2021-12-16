using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding
{
    /// <summary>
    /// Retrieve (or generate a new) deposit addresses for a particular asset and method.
    /// </summary>
    public class DepositAddressInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }
        
        [JsonProperty("result")]
        public DepositAddressInfoResult[] Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DepositAddressInfoResult
    {
        /// <summary>
        /// Deposit Address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Expiration time in unix timestamp, or 0 if not expiring
        /// </summary>
        [JsonProperty("expiretm")]
        public object ExpireTime { get; set; }

        /// <summary>
        /// Whether or not address has ever been used
        /// </summary>
        [JsonProperty("new")]
        public bool New { get; set; }
    }

}
