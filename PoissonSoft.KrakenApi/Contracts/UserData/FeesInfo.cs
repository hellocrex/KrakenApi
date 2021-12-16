using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    public class FeesInfo
    {
        /// <summary>
        /// Current fee (in percent)
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// minimum fee for pair (if not fixed fee)
        /// </summary>
        [JsonProperty("min_fee")]
        public decimal MinFee { get; set; }

        /// <summary>
        /// maximum fee for pair (if not fixed fee)
        /// </summary>
        [JsonProperty("max_fee")]
        public decimal MaxFee { get; set; }

        /// <summary>
        /// next tier's fee for pair (if not fixed fee, null if at lowest fee tier)
        /// </summary>
        [JsonProperty("next_fee")]
        public decimal? NextFee { get; set; }

        /// <summary>
        /// volume level of current tier (if not fixed fee. null if at lowest fee tier)
        /// </summary>
        [JsonProperty("tier_volume")]
        public decimal? TierVolume { get; set; }

        /// <summary>
        /// volume level of next tier (if not fixed fee. null if at lowest fee tier)
        /// </summary>
        [JsonProperty("next_volume")]
        public decimal? NextVolume { get; set; }
    }
}
