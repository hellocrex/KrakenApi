using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserTrading.Request
{
    /// <summary>
    /// Cancel a particular open order (or set of open orders) by txid or userref
    /// </summary>
    public class ReqCancelOrder
    {
        /// <summary>
        /// Nonce used in construction of API-Sign header
        /// </summary>
        [JsonProperty("nonce")]
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        /// <summary>
        /// Open order transaction ID (txid) or user reference (userref)
        /// </summary>
        [JsonProperty("txid")]
        public string TxId { get; set; } 
    }
}
