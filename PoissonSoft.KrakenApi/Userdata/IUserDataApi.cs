using PoissonSoft.KrakenApi.Contracts.UserData;
using PoissonSoft.KrakenApi.Contracts.UserData.Request;

namespace PoissonSoft.KrakenApi.Userdata
{
    public interface IUserDataApi
    {
        /// <summary>
        /// Retrieve all cash balances, net of pending withdrawals.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        AccountBalance GetAccountBalance(ReqEmpty req);

        /// <summary>
        /// Retrieve a summary of collateral balances, margin position valuations, equity and margin level.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        TradeBalance GetTradeBalance(ReqBalance req);

        /// <summary>
        /// Retrieve information about currently open orders.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        OpenOrders GetOpenOrders(ReqOrders req);

        /// <summary>
        /// Retrieve information about orders that have been closed (filled or cancelled). 50 results are returned at a time, the most recent by default.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        ClosedOrders GetClosedOrders(ReqOrders req);

        /// <summary>
        /// Retrieve information about specific orders.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        QueryOrdersInfo QueryOrdersInfo(ReqSpecificOrdersInfo req);

        /// <summary>
        /// Retrieve information about trades/fills. 50 results are returned at a time, the most recent by default.
        /// Unless otherwise stated, costs, fees, prices, and volumes are specified with the precision for the asset pair (pair_decimals and lot_decimals),
        /// not the individual assets' precision (decimals).
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        TradeHistory GetTradesHistory(ReqOrders req);

        /// <summary>
        /// Retrieve information about specific trades/fills.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        QueryTrades QueryTradesInfo(ReqTrades req);

        /// <summary>
        /// Retrieve information about ledger entries. 50 results are returned at a time, the most recent by default.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        LedgersInfo GetLedgersInfo(ReqOrders req);

        /// <summary>
        /// Retrieve information about specific ledger entries.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        QueryLedgers QueryLedgers(ReqLedgers req);

        /// <summary>
        /// Note: If an asset pair is on a maker/taker fee schedule, the taker side is given in fees and maker side in fees_maker.
        /// For pairs not on maker/taker, they will only be given in fees.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        TradeVolume GetTradeVolume(ReqTradeVolume req);
    }
}
