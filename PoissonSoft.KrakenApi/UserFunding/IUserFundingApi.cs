using PoissonSoft.KrakenApi.Contracts.UserFunding;
using PoissonSoft.KrakenApi.Contracts.UserFunding.Request;

namespace PoissonSoft.KrakenApi.UserFunding
{
    public interface IUserFundingApi
    {
        /// <summary>
        /// Retrieve methods available for depositing a particular asset.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        DepositMethodsInfo GetDepositMethods(ReqDepositMethod req);

        /// <summary>
        /// Retrieve (or generate a new) deposit addresses for a particular asset and method.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        DepositAddressInfo GetDepositAddresses(ReqDepositAddress req);

        /// <summary>
        /// Retrieve information about recent deposits made.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        SatatusOfRecent GetStatusOfRecentDeposits(ReqStatusOfRecent req);

        /// <summary>
        /// Retrieve fee information about potential withdrawals for a particular asset, key and amount.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        WithdrawalInfo GetWithdrawalInformation(ReqWithdrawalInfo req);

        /// <summary>
        /// Make a withdrawal request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        WithdrawFundsInfo WithdrawFunds(ReqWithdrawalInfo req);

        /// <summary>
        /// Retrieve information about recently requests withdrawals.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        SatatusOfRecent GetStatusOfRecentWithdrawals(ReqStatusOfRecent req);

        /// <summary>
        /// Cancel a recently requested withdrawal, if it has not already been successfully processed.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        WithdrawalCancelationInfo RequestWithdrawalCancelation(ReqWithdrawalCancel req);

        /// <summary>
        /// Transfer from Kraken spot wallet to Kraken Futures holding wallet.
        /// Note that a transfer in the other direction must be requested via the Kraken Futures API
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        WalletTransferInfo RequestWalletTransfer(ReqWalletTransfer req);
    }
}
