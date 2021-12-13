using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.MarketData
{
    public class OHLCData
    {
        public object[] error { get; set; }

        //[JsonProperty("result")]
        public Result result { get; set; }
    }

    //public class Result
    //{
    //    public Dictionary<string, ObjectType[][]> XXBTZUSD1 { get; set; }

    //    //  [JsonProperty("last")]
    //    public int Last { get; set; }
    //}


    public class ObjectType
    {
        public int Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
        public string Property5 { get; set; }
        public string Property6 { get; set; }
        public int Property7 { get; set; }
    }

    public class Result
    {
        public object[][] XXBTZUSD { get; set; }
        public int last { get; set; }
    }


}
