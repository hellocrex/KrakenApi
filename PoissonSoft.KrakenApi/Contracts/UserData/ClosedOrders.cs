using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    /// <summary>
    /// Retrieve information about orders that have been closed (filled or cancelled). 50 results are returned at a time, the most recent by default
    /// </summary>
    public class ClosedOrders
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }
        
        [JsonProperty("result")]
        public ClosedOrdersInfo Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ClosedOrdersInfo
    {
        /// <summary>
        /// Open Order
        /// </summary>
        [JsonProperty("closed")]
        public Dictionary<string, GeneralOrderInfo> Closed { get; set; }

        /// <summary>
        /// Amount of available order info matching criteria
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
    }

}
