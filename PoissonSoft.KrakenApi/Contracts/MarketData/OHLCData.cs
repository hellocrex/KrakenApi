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
        //public Dictionary<string, OHLCDataResult> Result { get; set; }
        public OHLCDataResult Result { get; set; }
    }

    public class OHLCDataResult
    {
        public object[][] XXBTZUSD { get; set; }
        //public object[][] Tick { get; set; }
        //public Dictionary<string, string[]> XXBTZUSD { get; set; }

        [JsonProperty("last")]
        public int Last { get; set; }
    }

}
