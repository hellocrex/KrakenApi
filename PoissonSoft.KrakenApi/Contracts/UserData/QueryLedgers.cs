using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    /// <summary>
    /// Retrieve information about specific ledger entries.
    /// </summary>
    public class QueryLedgers
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public Dictionary<string, Ledgers> Result { get; set; }
    }
}
