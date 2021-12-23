using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketDataStream
{
    public class ResponseMessage
    {
        /// <summary>
        /// errorMessage
        /// </summary>
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// event
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
