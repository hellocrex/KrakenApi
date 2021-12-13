using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData.Request
{
    public class ReqEmpty
    {
        /// <summary>
        /// Asset pair to get data for
        /// </summary>
        [JsonProperty("nonce")] 
        public long Nonce = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
    }
}