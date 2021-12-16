using System;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding.Request
{
    /// <summary>
    /// Retrieve information about recent deposits or withdrawal  made
    /// </summary>
    public class ReqStatusOfRecent
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
        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        public string Method { get; set; }
    }
}
