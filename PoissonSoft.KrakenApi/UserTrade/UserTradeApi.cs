using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using KrakenApi;
using NLog;
using PoissonSoft.KrakenApi.Contracts.UserData;
using PoissonSoft.KrakenApi.Contracts.UserData.Request;
using PoissonSoft.KrakenApi.Contracts.UserFunding.Request;
using PoissonSoft.KrakenApi.Contracts.UserTrading;
using PoissonSoft.KrakenApi.Contracts.UserTrading.Request;
using PoissonSoft.KrakenApi.Transport;
using PoissonSoft.KrakenApi.Transport.Rest;

namespace PoissonSoft.KrakenApi.UserTrade
{
    public class UserTradeApi: IUserTradeApi, IDisposable
    {
        private readonly KrakenApiClient apiClient;
        private readonly RestClient client;

        public UserTradeApi(KrakenApiClient apiClient, KrakenApiClientCredentials credentials, ILogger logger)
        {
            apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            client = new RestClient(logger, KrakenApiClient.Endpoint,
                new[] { EndpointSecurityType.Private }, credentials, apiClient.Throttler);
        }

        /// <inheritdoc />
        public NewOrderInfo AddNewOrder(ReqNewOrder req)
        {
            return client.MakeRequest<NewOrderInfo>(new RequestParameters(HttpMethod.Post, "0/private/AddOrder", 1, req));
        }

        /// <inheritdoc />
        public CancelOrderInfo CancelOrder(ReqCancelOrder req)
        {
            return client.MakeRequest<CancelOrderInfo>(new RequestParameters(HttpMethod.Post, "0/private/CancelOrder", 1, req));
        }

        /// <inheritdoc />
        public CancelOrderInfo CancelAllOrders(ReqEmpty req)
        {
            return client.MakeRequest<CancelOrderInfo>(new RequestParameters(HttpMethod.Post, "0/private/CancelAll", 1, req));
        }

        /// <inheritdoc />
        public CancelAllOrdersInfo CancelAllOrdersAfterX(ReqCancelAllAfterX req)
        {
            return client.MakeRequest<CancelAllOrdersInfo>(new RequestParameters(HttpMethod.Post, "0/private/CancelAllOrdersAfter", 1, req));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
