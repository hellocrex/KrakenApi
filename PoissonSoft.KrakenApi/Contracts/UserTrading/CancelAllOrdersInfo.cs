using System;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserTrading
{
    /// <summary>
    /// Cancel a particular open order (or set of open orders) by txid or userref
    /// </summary>
    public class CancelAllOrdersInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public CancelAllOrdersResult Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CancelAllOrdersResult
    {
        /// <summary>
        /// Timestamp (RFC3339 format) at which the request was received
        /// </summary>
        [JsonProperty("currentTime")]
        public DateTimeOffset CurrentTime { get; set; }

        /// <summary>
        /// Timestamp (RFC3339 format) after which all orders will be cancelled, unless the timer is extended or disabled
        /// </summary>
        [JsonProperty("triggerTime")]
        public DateTimeOffset TriggerTime { get; set; }
    }
}
