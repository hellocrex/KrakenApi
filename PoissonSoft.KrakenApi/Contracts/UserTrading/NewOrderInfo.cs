using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserTrading
{
    public class NewOrderInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public NewOrderResult Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NewOrderResult
    {
        /// <summary>
        /// Order description info
        /// </summary>
        [JsonProperty("descr")]
        public NewOrderDescr Description { get; set; }

        /// <summary>
        /// Transaction IDs for order
        /// </summary>
        [JsonProperty("txid")]
        public string[] OrderId { get; set; }
    }

    public class NewOrderDescr
    {
        /// <summary>
        /// Order description
        /// </summary>
        [JsonProperty("order")]
        public int Order{ get; set; }

        /// <summary>
        /// Conditional close order description, if applicable
        /// </summary>
        [JsonProperty("close")]
        public int Close { get; set; }
    }
}
