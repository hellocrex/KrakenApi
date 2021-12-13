using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserData.Request
{
    /// <summary>
    /// Retrieve information about currently open orders.
    /// </summary>
    public class ReqTrades
    {
        /// <summary>
        /// Nonce used in construction of API-Sign header
        /// </summary>
        [JsonProperty("nonce")] 
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        /// <summary>
        /// Comma delimited list of transaction IDs to query info about (20 maximum)
        /// </summary>
        [JsonProperty("txid", NullValueHandling = NullValueHandling.Ignore)]
        public string Txid { get; set; }

        /// <summary>
        /// Whether or not to include trades related to position in output
        /// </summary>
        [JsonProperty("trades", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Trades { get; set; }
    }
}