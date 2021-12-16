using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    public class LedgersInfo
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public LedgerResult Result { get; set; }
    }

    public class LedgerResult
    {
        [JsonProperty("ledger")]
        public Dictionary<string, Ledgers> Ledger { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }

}
