using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserData.Request
{
    /// <summary>
    /// Retrieve information about specific orders.
    /// </summary>
    public class ReqSpecificOrdersInfo
    {
        /// <summary>
        /// Nonce used in construction of API-Sign header
        /// </summary>
        [JsonProperty("nonce")] 
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        /// <summary>
        /// Restrict results to given user reference id
        /// </summary>
        [JsonProperty("userref", NullValueHandling = NullValueHandling.Ignore)]
        public int UserRef { get; set; }

        /// <summary>
        /// Whether or not to include trades related to position in output
        /// </summary>
        [JsonProperty("trades", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Trades { get; set; }

        /// <summary>
        /// Comma delimited list of transaction IDs to query info about (20 maximum)
        /// </summary>
        [JsonProperty("txid")]
        public string TxId { get; set; }
    }
}