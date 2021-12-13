using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData.Request
{
    public class ReqBalance
    {
        /// <summary>
        /// Asset pair to get data for
        /// </summary>
        [JsonProperty("nonce")] 
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        ///// <summary>
        ///// Asset pair to get data for
        ///// </summary>
        //[JsonProperty("asset")]
        //public string Asset { get; set; }
    }
}