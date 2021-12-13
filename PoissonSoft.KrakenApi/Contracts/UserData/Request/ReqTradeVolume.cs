using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserData.Request
{
    /// <summary>
    /// Note: If an asset pair is on a maker/taker fee schedule, the taker side is given in fees and maker side in fees_maker.
    /// For pairs not on maker/taker, they will only be given in fees.
    /// </summary>
    public class ReqTradeVolume
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("nonce")] 
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        /// <summary>
        /// Comma delimited list of asset pairs to get fee info on (optional)
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Instrument { get; set; }

        /// <summary>
        /// Whether or not to include fee info in results (optional)
        /// </summary>
        [JsonProperty("fee-info", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FeeInfo { get; set; }
    }
}