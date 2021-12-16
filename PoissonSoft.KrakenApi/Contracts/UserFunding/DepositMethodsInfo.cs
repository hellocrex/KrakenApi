using System.Collections.Generic;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.UserData;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding
{
    /// <summary>
    /// Retrieve methods available for depositing a particular asset
    /// </summary>
    public class DepositMethodsInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }
        
        [JsonProperty("result")]
        public DepositMethodsResult[] Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DepositMethodsResult
    {
        /// <summary>
        /// Name of deposit method
        /// </summary>
        [JsonProperty("method")]
        public string MethodName { get; set; }

        /// <summary>
        /// Maximum net amount that can be deposited right now, or false if no limit
        /// </summary>
        [JsonProperty("limit")]
        public object Limit { get; set; }

        /// <summary>
        /// Amount of fees that will be paid
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Whether or not method has an address setup fee
        /// </summary>
        [JsonProperty("address-setup-fee")]
        public string AddressSetupFee { get; set; }

        /// <summary>
        /// Whether new addresses can be generated for this method.
        /// </summary>
        [JsonProperty("gen-address")]
        public bool GenAddress { get; set; }
    }

}
