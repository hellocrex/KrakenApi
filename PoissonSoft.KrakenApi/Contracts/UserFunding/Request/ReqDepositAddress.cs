using System;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding.Request
{
    /// <summary>
    /// Retrieve (or generate a new) deposit addresses for a particular asset and method.
    /// </summary>
    public class ReqDepositAddress
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

        /// <summary>
        /// Name of the deposit method
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// Whether or not to generate a new address
        /// </summary>
        [JsonProperty("new", NullValueHandling = NullValueHandling.Ignore)]
        public bool? New { get; set; }
    }
}
