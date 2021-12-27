using System;
using System.Net.Http;
using KrakenApi;
using NLog;
using PoissonSoft.KrakenApi.Contracts.UserData;
using PoissonSoft.KrakenApi.Contracts.UserData.Request;
using PoissonSoft.KrakenApi.Transport;
using PoissonSoft.KrakenApi.Transport.Rest;

namespace PoissonSoft.KrakenApi.Userdata
{
    public class UserDataApi: IUserDataApi, IDisposable
    {
        private readonly KrakenApiClient apiClient;
        private readonly RestClient client;

        public UserDataApi(KrakenApiClient apiClient, KrakenApiClientCredentials credentials, ILogger logger)
        {
            apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            client = new RestClient(logger, KrakenApiClient.Endpoint,
                new[] { EndpointSecurityType.Private }, credentials, apiClient.Throttler);

        }

        /// <inheritdoc />
        public AccountBalance GetAccountBalance(ReqEmpty req)
        {
            return client.MakeRequest<AccountBalance>(new RequestParameters(HttpMethod.Post, "0/private/Balance", 1, req));
        }

        /// <inheritdoc />
        public TradeBalance GetTradeBalance(ReqBalance req)
        {
            return client.MakeRequest<TradeBalance>(new RequestParameters(HttpMethod.Post, "0/private/TradeBalance", 1, req));
        }

        /// <inheritdoc />
        public OpenOrders GetOpenOrders(ReqOrders req)
        {
            return client.MakeRequest<OpenOrders>(new RequestParameters(HttpMethod.Post, "0/private/OpenOrders", 1, req));
        }

        /// <inheritdoc />
        public ClosedOrders GetClosedOrders(ReqOrders req)
        {
            return client.MakeRequest<ClosedOrders>(new RequestParameters(HttpMethod.Post, "0/private/ClosedOrders", 1, req));
        }

        /// <inheritdoc />
        public QueryOrdersInfo QueryOrdersInfo(ReqSpecificOrdersInfo req)
        {
            return client.MakeRequest<QueryOrdersInfo>(new RequestParameters(HttpMethod.Post, "0/private/QueryOrders", 1, req));
        }

        /// <inheritdoc />
        public TradeHistory GetTradesHistory(ReqOrders req)
        {
            return client.MakeRequest<TradeHistory>(new RequestParameters(HttpMethod.Post, "0/private/TradesHistory", 2, req, true));
        }

        /// <inheritdoc />
        public QueryTrades QueryTradesInfo(ReqTrades req)
        {
            return client.MakeRequest<QueryTrades>(new RequestParameters(HttpMethod.Post, "0/private/QueryTrades", 1, req));
        }

        /// <inheritdoc />
        public LedgersInfo GetLedgersInfo(ReqOrders req)
        {
            return client.MakeRequest<LedgersInfo>(new RequestParameters(HttpMethod.Post, "0/private/Ledgers", 1, req));
        }

        /// <inheritdoc />
        public QueryLedgers QueryLedgers(ReqLedgers req)
        {
            return client.MakeRequest<QueryLedgers>(new RequestParameters(HttpMethod.Post, "0/private/QueryLedgers", 1, req));
        }

        /// <inheritdoc />
        public TradeVolume GetTradeVolume(ReqTradeVolume req)
        {
            return client.MakeRequest<TradeVolume>(new RequestParameters(HttpMethod.Post, "0/private/TradeVolume", 1, req));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
