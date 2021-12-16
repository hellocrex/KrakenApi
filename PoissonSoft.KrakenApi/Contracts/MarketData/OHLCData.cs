using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData
{
    /// <summary>
    /// 
    /// </summary>
    public class OHLCData
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }

        [JsonProperty("result")]
        public OHLCDataResult Result { get; set; }
    }

    public class OHLCDataResult
    {
        public object[][] XXBTZUSD { get; set; }
        //public Dictionary<string[], object>[][] XXBTZUSD { get; set; }

        [JsonProperty("last")]
        public int last { get; set; }
    }

}
