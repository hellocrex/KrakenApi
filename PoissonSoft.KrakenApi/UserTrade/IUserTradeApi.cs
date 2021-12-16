using PoissonSoft.KrakenApi.Contracts.UserData.Request;
using PoissonSoft.KrakenApi.Contracts.UserTrading;
using PoissonSoft.KrakenApi.Contracts.UserTrading.Request;

namespace PoissonSoft.KrakenApi.UserTrade
{
    public interface IUserTradeApi
    {
        /// <summary>
        /// Place a new order.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        NewOrderInfo AddNewOrder(ReqNewOrder req);

        /// <summary>
        /// Cancel a particular open order (or set of open orders) by txid or userref
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        CancelOrderInfo CancelOrder(ReqCancelOrder req);

        /// <summary>
        /// Cancel all open orders
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        CancelOrderInfo CancelAllOrders(ReqEmpty req);

        /// <summary>
        /// CancelAllOrdersAfter provides a "Dead Man's Switch" mechanism to protect the client from network malfunction,
        /// extreme latency or unexpected matching engine downtime. 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        CancelAllOrdersInfo CancelAllOrdersAfterX(ReqCancelAllAfterX req);
    }
}
