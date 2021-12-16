using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserTrading
{
    /// <summary>
    /// Cancel a particular open order (or set of open orders) by txid or userref
    /// </summary>
    public class CancelOrderInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public CancelOrderResult Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CancelOrderResult
    {
        /// <summary>
        /// Number of orders cancelled.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// if set, order(s) is/are pending cancellation
        /// </summary>
        [JsonProperty("pending")]
        public bool Pending { get; set; }
    }
}
