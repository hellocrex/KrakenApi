using System;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding.Request
{
    /// <summary>
    /// Retrieve fee information about potential withdrawals for a particular asset, key and amount.
    /// </summary>
    public class ReqWithdrawalInfo
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
        /// Withdrawal key name, as set up on your account
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// Amount to be withdrawn
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
