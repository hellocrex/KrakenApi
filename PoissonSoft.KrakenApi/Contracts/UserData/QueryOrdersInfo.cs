using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    /// <summary>
    /// Retrieve information about specific orders.
    /// </summary>
    public class QueryOrdersInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public Dictionary<string, GeneralOrderInfo> Result { get; set; }
    }
}
