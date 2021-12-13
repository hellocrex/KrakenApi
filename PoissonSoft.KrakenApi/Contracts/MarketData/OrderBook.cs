using System;
using System.Collections.Generic;
using System.Text;

namespace PoissonSoft.KrakenApi.Contracts.MarketData
{
    public class OrderBook
    {
        public object[] error { get; set; }
        public Dictionary<string, OrderBookInfo> result { get; set; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class OrderBookInfo
    {
        public object[][] asks { get; set; }
        public object[][] bids { get; set; }
    }

}
