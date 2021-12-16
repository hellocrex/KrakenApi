using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding.Request
{
    /// <summary>
    /// Transfer from Kraken spot wallet to Kraken Futures holding wallet.
    /// Note that a transfer in the other direction must be requested via the Kraken Futures API endpoint.
    /// </summary>
    public class ReqWalletTransfer
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
        /// Source wallet
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Destination wallet
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// Amount to transfer
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
