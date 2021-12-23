using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.DataStream
{
    public class InstanceServersData
    {
        /// <summary>
        /// Websocket server address for establishing connection
        /// </summary>
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        /// <summary>
        /// Indicate whether SSL encryption is used
        /// </summary>
        [JsonProperty("encrypt")]
        public bool Encrypt { get; set; }

        /// <summary>
        /// Protocol supported
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// Recommended to send ping interval in millisecond
        /// </summary>
        [JsonProperty("pingInterval")]
        public uint pingInterval { get; set; }

        /// <summary>
        /// After such a long time(millisecond), if you do not receive pong, it will be considered as disconnected
        /// </summary>
        [JsonProperty("pingTimeout")]
        public uint pingTimeout { get; set; }
    }
}
