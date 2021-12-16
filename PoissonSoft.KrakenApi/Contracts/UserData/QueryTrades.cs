using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    public class QueryTrades
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public Dictionary<string, TradeHistoryInfo> Result { get; set; }
    }
}
