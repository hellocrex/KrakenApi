using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.DataStream
{
    public class ConnectionInfo
    {
        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("instanceServers")]
        public InstanceServersData[] InstanceData { get; set; }
    }
}
