using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    public class TradeHistory
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public HistoryResult Result { get; set; }
    }

    public class HistoryResult
    {
        [JsonProperty("trades")]
        public Dictionary<string, TradeHistoryInfo> Trades { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
