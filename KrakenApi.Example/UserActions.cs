using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KrakenApi.Contracts.Enums;
using PoissonSoft.KrakenApi.Contracts.UserData.Request;
using PoissonSoft.KrakenApi.Contracts.UserFunding.Request;
using PoissonSoft.KrakenApi.Contracts.UserTrading.Request;

namespace KrakenApi.Example
{
    internal partial class ActionManager
    {
        #region UserData

        private bool ShowUserDataPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Account Balance",
                [ConsoleKey.B] = "Trade Balance",
                [ConsoleKey.C] = "Open Orders",
                [ConsoleKey.D] = "Closed Orders",
                [ConsoleKey.E] = "Query Orders Info",
                [ConsoleKey.F] = "Trade History",
                [ConsoleKey.G] = "Query Trades Info",
                [ConsoleKey.H] = "Open Position",
                [ConsoleKey.I] = "Ledgers Info",
                [ConsoleKey.J] = "Query Ledgers",
                [ConsoleKey.K] = "Trade Volume",
                [ConsoleKey.L] = "Request Export Report",
                [ConsoleKey.M] = "Export Report Status",
                [ConsoleKey.N] = "Retrieve Data Export",
                [ConsoleKey.O] = "Delete Export Report",
                [ConsoleKey.Escape] = "Go back (to main menu)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A:
                    SafeCall(() =>
                    {
                        //for (int i = 0; i < 500; i++)
                        //{
                            var accountBalanceInfo = apiClient.UserDataApi.GetAccountBalance(new ReqEmpty());
                            Console.WriteLine(JsonConvert.SerializeObject(accountBalanceInfo, Formatting.Indented));
                        //}
                        
                    });
                    return true;

                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        var req = new ReqBalance
                        {
                            Ticker = InputHelper.GetString("Asset: ")
                        };
                        var tradeBalanceInfo = apiClient.UserDataApi.GetTradeBalance(req);
                        Console.WriteLine(JsonConvert.SerializeObject(tradeBalanceInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C:
                    SafeCall(() =>
                    {
                        var req = new ReqOrders
                        {
                            Trades = true
                        };
                        var openOrdersInfo = apiClient.UserDataApi.GetOpenOrders(req);
                        Console.WriteLine(JsonConvert.SerializeObject(openOrdersInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.D:
                    SafeCall(() =>
                    {
                        var req = new ReqOrders
                        {
                            Trades = true
                        };
                        var closedOrdersInfo = apiClient.UserDataApi.GetClosedOrders(req);
                        Console.WriteLine(JsonConvert.SerializeObject(closedOrdersInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.E:
                    SafeCall(() =>
                    {
                        var req = new ReqSpecificOrdersInfo
                        {
                            Trades = true,
                            TxId = InputHelper.GetString("Comma delimited list of transaction IDs to query info about (20 maximum): ")
                        };
                        var queryOrdersInfo = apiClient.UserDataApi.QueryOrdersInfo(req);
                        Console.WriteLine(JsonConvert.SerializeObject(queryOrdersInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.F:
                    SafeCall(() =>
                    {
                        for (int i = 1; i < 9; i++)
                        {
                            var req = new ReqOrders
                            {
                                Trades = true
                            };
                            var tradesHistoryInfo = apiClient.UserDataApi.GetTradesHistory(req);
                            Console.WriteLine($"Номер итерации {i}, Iteration start {DateTimeOffset.Now}");
                            Console.WriteLine(JsonConvert.SerializeObject(tradesHistoryInfo, Formatting.Indented));
                        }

                    });
                    return true;

                case ConsoleKey.G:
                    SafeCall(() =>
                    {
                        var req = new ReqTrades
                        {
                            Txid = InputHelper.GetString("Comma delimited list of transaction IDs to query info about (20 maximum): ")
                        };
                        var queryTradesInfo = apiClient.UserDataApi.QueryTradesInfo(req);
                        Console.WriteLine(JsonConvert.SerializeObject(queryTradesInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.I:
                    SafeCall(() =>
                    {
                        var ledgersInfoInfo = apiClient.UserDataApi.GetLedgersInfo(new ReqOrders());
                        Console.WriteLine(JsonConvert.SerializeObject(ledgersInfoInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.J:
                    SafeCall(() =>
                    {
                        var req = new ReqLedgers
                        {
                            Id = InputHelper.GetString("Comma delimited list of ledger IDs to query info about (20 maximum): ")
                        };
                        var queryLedgersInfo = apiClient.UserDataApi.QueryLedgers(req);
                        Console.WriteLine(JsonConvert.SerializeObject(queryLedgersInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.K:
                    SafeCall(() =>
                    {
                        var req = new ReqTradeVolume
                        {
                            Instrument = InputHelper.GetString("Comma delimited list of asset pairs to get fee info on (optional): ")
                        };
                        var tradeVolumeInfo = apiClient.UserDataApi.GetTradeVolume(req);
                        Console.WriteLine(JsonConvert.SerializeObject(tradeVolumeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    if (actions.ContainsKey(selectedAction))
                    {
                        Console.WriteLine($"Method '{actions[selectedAction]}' is not implemented");
                    }
                    return true;
            }
        }

        #endregion

        #region UserTrading

        private bool ShowUserTradingPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Add Order",
                [ConsoleKey.B] = "Cancel Order",
                [ConsoleKey.C] = "cancel All Orders",
                [ConsoleKey.D] = "Cancel All Orders After X",
                [ConsoleKey.Escape] = "Go back (to main menu)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A:
                    SafeCall(() =>
                    {
                        var req = new ReqNewOrder
                        {
                            OrderType = InputHelper.GetEnum<OrderType>("Order type"),
                            Instrument = InputHelper.GetString("Instrument: "),
                            OrderSide = InputHelper.GetEnum<OrderSide>("Order direction (buy/sell)"),
                            Volume = InputHelper.GetDecimal("Order quantity in terms of the base asset"),
                            Price = InputHelper.GetDecimal("Price")
                        };
                        var exchangeInfo = apiClient.UserTradeApi.AddNewOrder(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        var req = new ReqCancelOrder
                        {
                            TxId = InputHelper.GetString("Open order transaction ID (txid) or user reference (userref): "),
                        };
                        var cancelOrderInfo = apiClient.UserTradeApi.CancelOrder(req);
                        Console.WriteLine(JsonConvert.SerializeObject(cancelOrderInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C:
                    SafeCall(() =>
                    {
                        var cancelOrderInfo = apiClient.UserTradeApi.CancelAllOrders(new ReqEmpty());
                        Console.WriteLine(JsonConvert.SerializeObject(cancelOrderInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.D:
                    SafeCall(() =>
                    {
                        var req = new ReqCancelAllAfterX
                        {
                            Timeout = InputHelper.GetInt("Duration (in seconds) to set/extend the timer by: "),
                        };
                        var cancelOrderInfo = apiClient.UserTradeApi.CancelAllOrdersAfterX(req);
                        Console.WriteLine(JsonConvert.SerializeObject(cancelOrderInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }

        #endregion

        #region UserFunding

        private bool ShowUserFundingPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Get Deposit Methods",
                [ConsoleKey.B] = "Get Deposit Addresses",
                [ConsoleKey.C] = "Get Status of Recent Deposits",
                [ConsoleKey.D] = "Get Withdrawal Information",
                [ConsoleKey.E] = "Withdraw Funds",
                [ConsoleKey.F] = "Get Status of Recent Withdrawals",
                [ConsoleKey.G] = "Request Withdrawal Cancelation",
                [ConsoleKey.H] = "Request Wallet Transfer",
                [ConsoleKey.Escape] = "Go back (to main menu)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A:
                    SafeCall(() =>
                    {
                        var req = new ReqDepositMethod
                        {
                            Ticker = InputHelper.GetString("Ticker: ")
                        };
                        var depositMethodsInfo = apiClient.UserFundingApi.GetDepositMethods(req);
                        Console.WriteLine(JsonConvert.SerializeObject(depositMethodsInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        var req = new ReqDepositAddress
                        {
                            Ticker = InputHelper.GetString("Ticker: "),
                            Method = InputHelper.GetString("Name of the deposit method: "),
                            New = false
                        };
                        var depositAddressesInfo = apiClient.UserFundingApi.GetDepositAddresses(req);
                        Console.WriteLine(JsonConvert.SerializeObject(depositAddressesInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C:
                    SafeCall(() =>
                    {
                        var req = new ReqStatusOfRecent()
                        {
                            Ticker = InputHelper.GetString("Ticker: "),
                            Method = InputHelper.GetString("Name of the deposit method: ")
                        };
                        var statusOfRecentDepositsInfo = apiClient.UserFundingApi.GetStatusOfRecentDeposits(req);
                        Console.WriteLine(JsonConvert.SerializeObject(statusOfRecentDepositsInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.D:
                    SafeCall(() =>
                    {
                        var req = new ReqWithdrawalInfo()
                        {
                            Ticker = InputHelper.GetString("Ticker: "),
                            Key = InputHelper.GetString("Withdrawal key name, as set up on your account: "),
                            Amount = InputHelper.GetInt("Amount to be withdrawn: ")
                        };
                        var withdrawalInfo = apiClient.UserFundingApi.GetWithdrawalInformation(req);
                        Console.WriteLine(JsonConvert.SerializeObject(withdrawalInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.E:
                    SafeCall(() =>
                    {
                        var req = new ReqWithdrawalInfo()
                        {
                            Ticker = InputHelper.GetString("Ticker: "),
                            Key = InputHelper.GetString("Withdrawal key name, as set up on your account: "),
                            Amount = InputHelper.GetInt("Amount to be withdrawn: ")
                        };
                        var withdrawFundsInfo = apiClient.UserFundingApi.WithdrawFunds(req);
                        Console.WriteLine(JsonConvert.SerializeObject(withdrawFundsInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.F:
                    SafeCall(() =>
                    {
                        var req = new ReqStatusOfRecent
                        {
                            Ticker = InputHelper.GetString("Ticker: "),
                            Method = InputHelper.GetString("Name of the withdrawal method: ")
                        };
                        var statusOfRecentWithdrawalsInfo = apiClient.UserFundingApi.GetStatusOfRecentWithdrawals(req);
                        Console.WriteLine(JsonConvert.SerializeObject(statusOfRecentWithdrawalsInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.G:
                    SafeCall(() =>
                    {
                        var req = new ReqWithdrawalCancel
                        {
                            Ticker = InputHelper.GetString("Ticker: "),
                            RefId = InputHelper.GetString("Withdrawal reference ID: ")
                        };
                        var withdrawalCancelation = apiClient.UserFundingApi.RequestWithdrawalCancelation(req);
                        Console.WriteLine(JsonConvert.SerializeObject(withdrawalCancelation, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.H:
                    SafeCall(() =>
                    {
                        var req = new ReqWalletTransfer
                        {
                            Ticker = InputHelper.GetString("Ticker: "),
                            From = InputHelper.GetString("Source wallet: "),
                            To = InputHelper.GetString("Destination wallet: "),
                            Amount = InputHelper.GetInt("Amount to transfer: ")
                        };
                        var walletTransferInfo = apiClient.UserFundingApi.RequestWalletTransfer(req);
                        Console.WriteLine(JsonConvert.SerializeObject(walletTransferInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    if (actions.ContainsKey(selectedAction))
                    {
                        Console.WriteLine($"Method '{actions[selectedAction]}' is not implemented");
                    }
                    return true;
            }
        }

        #endregion

        #region UserStaking

        private bool ShowUserStakingPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Stake Asset",
                [ConsoleKey.B] = "Unstake Asset",
                [ConsoleKey.C] = "List of Stakeable Assets",
                [ConsoleKey.D] = "Get Pending Staking Transactions",
                [ConsoleKey.E] = "List of Staking Transactions",
                [ConsoleKey.Escape] = "Go back (to main menu)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.Escape:
                    return false;
                default:
                    if (actions.ContainsKey(selectedAction))
                    {
                        Console.WriteLine($"Method '{actions[selectedAction]}' is not implemented");
                    }
                    return true;
            }
        }

        #endregion
    }
}
