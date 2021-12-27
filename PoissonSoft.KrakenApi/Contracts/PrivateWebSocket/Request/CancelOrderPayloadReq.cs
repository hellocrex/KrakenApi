using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.PrivateWebSocket.Request
{
    public class CancelOrderPayloadReq
    {
        /// <summary>
        /// cancelOrder
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Optional - client originated requestID sent as acknowledgment in the message response
        /// </summary>
        [JsonProperty("reqid", NullValueHandling = NullValueHandling.Ignore)]
        public long RequestId { get; set; }

        /// <summary>
        /// Array of order IDs to be canceled. These can be user reference IDs.
        /// </summary>
        [JsonProperty("txid", NullValueHandling = NullValueHandling.Ignore)]
        public string[] TxId { get; set; }
    }
}
