using PoissonSoft.KrakenApi.Contracts.MarketData;
using PoissonSoft.KrakenApi.Contracts.MarketData.Request;

namespace PoissonSoft.KrakenApi.MarketData
{
    public interface IMarketDataApi
    {
        /// <summary>
        /// Get tradable asset pairs
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        InstrumentInformation GetTradableAssetPairs(ReqInstrumentInformation req);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        TickerInformation GetTickerInformation(ReqInstrumentInformation req);

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
