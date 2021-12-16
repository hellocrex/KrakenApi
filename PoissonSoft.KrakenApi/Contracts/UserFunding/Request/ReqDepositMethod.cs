using System;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding.Request
{
    /// <summary>
    /// Retrieve methods available for depositing a particular asset.
    /// </summary>
    public class ReqDepositMethod
    {
        /// <summary>
        /// Nonce used in construction of API-Sign header
        /// </summary>
        [JsonProperty("nonce")]
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        /// <summary>
        /// Asset being deposited
        /// </summary>
        [JsonProperty("asset")]
        public string Ticker { get; set; }
    }
}
