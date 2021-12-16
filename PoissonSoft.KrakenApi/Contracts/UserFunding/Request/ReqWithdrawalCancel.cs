using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding.Request
{
    /// <summary>
    /// Cancel a recently requested withdrawal, if it has not already been successfully processed.
    /// </summary>
    public class ReqWithdrawalCancel
    {
        /// <summary>
        /// Nonce used in construction of API-Sign header
        /// </summary>
        [JsonProperty("nonce")]
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        /// <summary>
        /// Asset being withdrawn
        /// </summary>
        [JsonProperty("asset")]
        public string Ticker { get; set; }

        /// <summary>
        /// Withdrawal reference ID
        /// </summary>
        [JsonProperty("refid")]
        public string RefId { get; set; }
    }
}
