using System;
using System.Net.Http;
using KrakenApi;
using NLog;
using PoissonSoft.KrakenApi.Contracts.UserFunding;
using PoissonSoft.KrakenApi.Contracts.UserFunding.Request;
using PoissonSoft.KrakenApi.Transport;
using PoissonSoft.KrakenApi.Transport.Rest;

namespace PoissonSoft.KrakenApi.UserFunding
{
    public class UserFundingApi: IUserFundingApi, IDisposable
    {
        private readonly KrakenApiClient apiClient;
        private readonly RestClient client;

        public UserFundingApi(KrakenApiClient apiClient, KrakenApiClientCredentials credentials, ILogger logger)
        {
            apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            client = new RestClient(logger, KrakenApiClient.Endpoint,
                new[] { EndpointSecurityType.Private }, credentials, apiClient.Throttler);
        }

        /// <inheritdoc />
        public DepositMethodsInfo GetDepositMethods(ReqDepositMethod req)
        {
            return client.MakeRequest<DepositMethodsInfo>(new RequestParameters(HttpMethod.Post, "0/private/DepositMethods", 1, req));
        }

        /// <inheritdoc />
        public DepositAddressInfo GetDepositAddresses(ReqDepositAddress req)
        {
            return client.MakeRequest<DepositAddressInfo>(new RequestParameters(HttpMethod.Post, "0/private/DepositAddresses", 1, req));
        }

        /// <inheritdoc />
        public SatatusOfRecent GetStatusOfRecentDeposits(ReqStatusOfRecent req)
        {
            return client.MakeRequest<SatatusOfRecent>(new RequestParameters(HttpMethod.Post, "0/private/DepositStatus", 1, req));
        }

        /// <inheritdoc />
        public WithdrawalInfo GetWithdrawalInformation(ReqWithdrawalInfo req)
        {
            return client.MakeRequest<WithdrawalInfo>(new RequestParameters(HttpMethod.Post, "0/private/WithdrawInfo", 1, req));
        }

        /// <inheritdoc />
        public WithdrawFundsInfo WithdrawFunds(ReqWithdrawalInfo req)
        {
            return client.MakeRequest<WithdrawFundsInfo>(new RequestParameters(HttpMethod.Post, "0/private/Withdraw", 1, req));
        }

        /// <inheritdoc />
        public SatatusOfRecent GetStatusOfRecentWithdrawals(ReqStatusOfRecent req)
        {
            return client.MakeRequest<SatatusOfRecent>(new RequestParameters(HttpMethod.Post, "0/private/WithdrawStatus", 1, req));
        }

        /// <inheritdoc />
        public WithdrawalCancelationInfo RequestWithdrawalCancelation(ReqWithdrawalCancel req)
        {
            return client.MakeRequest<WithdrawalCancelationInfo>(new RequestParameters(HttpMethod.Post, "0/private/WithdrawCancel", 1, req));
        }

        /// <inheritdoc />
        public WalletTransferInfo RequestWalletTransfer(ReqWalletTransfer req)
        {
            return client.MakeRequest<WalletTransferInfo>(new RequestParameters(HttpMethod.Post, "0/private/WalletTransfer", 1, req));
        }


        /// <inheritdoc />
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
