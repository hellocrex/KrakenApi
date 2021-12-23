using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.PublicWebSocket
{
    public class WebsocketsToken
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public TokenResult Result { get; set; }
    }

    public class TokenResult
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Time (in seconds) after which the token expires
        /// </summary>
        [JsonProperty("expires")]
        public int TimeOut { get; set; }
    }
}
