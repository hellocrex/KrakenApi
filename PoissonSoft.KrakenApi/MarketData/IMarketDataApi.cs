using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KrakenApi.Contracts;
using PoissonSoft.KrakenApi.Contracts.MarketData;
using PoissonSoft.KrakenApi.Contracts.MarketData.Request;

namespace PoissonSoft.KrakenApi.MarketData
{
    public interface IMarketDataApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        TickerInformation GetTickerInformation(ReqTickerInformation req);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        OHLCData GetOHLCData(ReqOHLCData req);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        OrderBook GetOrderBook(ReqOrderBook req);
    }
}
