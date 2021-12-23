using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.DataStream
{
    /// <summary>
    /// Response to Create Listen Key Request
    /// </summary>
    public class CreateConnectTokenResponse
    {
        /// <summary>
        /// System error codes
        /// </summary>
        [JsonProperty("code")]
        public int SystemCode { get; set; }
        /// <summary>
        /// Listen Key
        /// </summary>
        [JsonProperty("data")]
        public ConnectionInfo Data { get; set; }
    }
}
