using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KrakenApi.Contracts.MarketData.Request;
using PoissonSoft.KrakenApi.Contracts.UserData.Request;

namespace KrakenApi.Example
{
    internal partial class ActionManager
    {
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
                        var exchangeInfo = apiClient.UserDataApi.GetAccountBalance(new ReqEmpty());
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        var exchangeInfo = apiClient.UserDataApi.GetTradeBalance(new ReqBalance
                        {
                          //  Asset = InputHelper.GetString("Asset: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C:
                    SafeCall(() =>
                    {
                        var exchangeInfo = apiClient.UserDataApi.GetOpenOrders(new ReqOrders
                        {
                            //  Asset = InputHelper.GetString("Asset: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.D:
                    SafeCall(() =>
                    {
                        var exchangeInfo = apiClient.UserDataApi.GetClosedOrders(new ReqOrders
                        {
                            //  Asset = InputHelper.GetString("Asset: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.O:
                    SafeCall(() =>
                    {
                        var req = new ReqOrderBook()
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetOrderBook(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }

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
                case ConsoleKey.I:
                    SafeCall(() =>
                    {
                        var req = new ReqTickerInformation()
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetTickerInformation(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        var req = new ReqOHLCData()
                        {
                            Instrument = InputHelper.GetString("Instrument: "),
                            //Interval = InputHelper.GetEnum<TimeInterval>("Interval: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetOHLCData(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.O:
                    SafeCall(() =>
                    {
                        var req = new ReqOrderBook()
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetOrderBook(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }

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
                case ConsoleKey.I:
                    SafeCall(() =>
                    {
                        var req = new ReqTickerInformation()
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetTickerInformation(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        var req = new ReqOHLCData()
                        {
                            Instrument = InputHelper.GetString("Instrument: "),
                            //Interval = InputHelper.GetEnum<TimeInterval>("Interval: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetOHLCData(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.O:
                    SafeCall(() =>
                    {
                        var req = new ReqOrderBook()
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetOrderBook(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }

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
                case ConsoleKey.I:
                    SafeCall(() =>
                    {
                        var req = new ReqTickerInformation()
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetTickerInformation(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        var req = new ReqOHLCData()
                        {
                            Instrument = InputHelper.GetString("Instrument: "),
                            //Interval = InputHelper.GetEnum<TimeInterval>("Interval: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetOHLCData(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.O:
                    SafeCall(() =>
                    {
                        var req = new ReqOrderBook()
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var exchangeInfo = apiClient.MarketDataApi.GetOrderBook(req);
                        Console.WriteLine(JsonConvert.SerializeObject(exchangeInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }

        #endregion
    }
}
