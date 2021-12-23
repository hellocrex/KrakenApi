using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.PrivateWebSocket
{
    public class CancelOrderPayload
    {
        /// <summary>
        /// addOrderStatus
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }

        /// <summary>
        /// Optional - client originated requestID sent as acknowledgment in the message response
        /// </summary>
        [JsonProperty("reqid", NullValueHandling = NullValueHandling.Ignore)]
        public string ReqId { get; set; }

        /// <summary>
        /// Status. "ok" or "error"
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// order ID (if successful)
        /// </summary>
        [JsonProperty("txid")]
        public string TxId { get; set; }

        /// <summary>
        /// order description info (if successful)
        /// </summary>
        [JsonProperty("descr")]
        public string Descr { get; set; }

        /// <summary>
        /// error message (if unsuccessful)
        /// </summary>
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
