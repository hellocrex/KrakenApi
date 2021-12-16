using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserFunding
{
    /// <summary>
    /// Cancel a recently requested withdrawal, if it has not already been successfully processed.
    /// </summary>
    public class WithdrawalCancelationInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        /// <summary>
        /// Whether cancellation was successful or not.
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
