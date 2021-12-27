using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.PrivateWebSocket
{
    public class CancelOrderPayload
    {
        /// <summary>
        /// cancelOrder
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }

        /// <summary>
        /// Optional - client originated requestID sent as acknowledgment in the message response
        /// </summary>
        [JsonProperty("reqid", NullValueHandling = NullValueHandling.Ignore)]
        public long RequestId { get; set; }

        /// <summary>
        /// Number of orders cancelled.
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int? Count { get; set; }

        /// <summary>
        /// Status. "ok" or "error"
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// error message (if unsuccessful)
        /// </summary>
        [JsonProperty("errorMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }
    }
}
