using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KrakenApi.Contracts.Enums;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    /// <summary>
    /// Retrieve a summary of collateral balances, margin position valuations, equity and margin level.
    /// </summary>
    public class OpenOrders
    {
        [JsonProperty("error")]
        public string[] Error { get; set; }
        
        [JsonProperty("result")]
        public OpenOrdersInfo Result { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OpenOrdersInfo
    {
        /// <summary>
        /// Open Order
        /// </summary>
        [JsonProperty("open")]
        public Dictionary<string, GeneralOrderInfo> Open { get; set; }
    }

}
