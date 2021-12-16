using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding
{
    /// <summary>
    /// Transfer from Kraken spot wallet to Kraken Futures holding wallet.
    /// Note that a transfer in the other direction must be requested via the Kraken Futures API endpoint.
    /// </summary>
    public class WalletTransferInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        /// <summary>
        /// Whether cancellation was successful or not.
        /// </summary>
        [JsonProperty("result")]
        public WalletTransferResult Result { get; set; }
    }

    public class WalletTransferResult
    {
        /// <summary>
        /// Reference ID
        /// </summary>
        [JsonProperty("refid")]
        public string RefId { get; set; }
    }
}
