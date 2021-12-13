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
    public class ReqOrders
    {
        /// <summary>
        /// Nonce used in construction of API-Sign header
        /// </summary>
        [JsonProperty("nonce")] 
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        /// <summary>
        /// Whether or not to include trades related to position in output
        /// </summary>
        [JsonProperty("trades", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Trades { get; set; }

        /// <summary>
        /// Restrict results to given user reference id
        /// </summary>
        [JsonProperty("userref", NullValueHandling = NullValueHandling.Ignore)]
        public int? Userref { get; set; }

        /// <summary>
        /// Starting unix timestamp or order tx ID of results (exclusive)
        /// </summary>
        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public int? StartTimeStamp { get; set; }

        /// <summary>
        /// Ending unix timestamp or order tx ID of results (inclusive)
        /// </summary>
        [JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
        public int? EndTimeStamp { get; set; }

        /// <summary>
        /// Result offset for pagination
        /// </summary>
        [JsonProperty("ofs", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        /// <summary>
        /// Which time to use to search
        /// Default: "both"
        /// </summary>
        [JsonProperty("closetime", NullValueHandling = NullValueHandling.Ignore)]
        public CloseTime? Closetime { get; set; }
    }
}