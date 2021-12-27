using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KrakenApi.Contracts.Enums;
using PoissonSoft.KrakenApi.Contracts.MarketData.Request;

namespace KrakenApi.Example
{
    internal partial class ActionManager
    {
        #region MarketData
        private bool ShowMarketDataPage()
        {
            var actions = new Dictionary<ConsoleKey, string>
            {
                [ConsoleKey.C] = "Tradable Asset Pairs Information",
                [ConsoleKey.I] = "Ticker Information",
                [ConsoleKey.B] = "OHCL Data",
                [ConsoleKey.O] = "Order Book",
                [ConsoleKey.Escape] = "Go back (to main menu)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.C:
                    SafeCall(() =>
                    {
                        var req = new ReqInstrumentInformation
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var AssetPairsInfo = apiClient.MarketDataApi.GetTradableAssetPairs(req);
                        Console.WriteLine(JsonConvert.SerializeObject(AssetPairsInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.I:
                    SafeCall(() =>
                    {
                        var req = new ReqInstrumentInformation
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var instrumentInfo = apiClient.MarketDataApi.GetTickerInformation(req);
                        Console.WriteLine(JsonConvert.SerializeObject(instrumentInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        var req = new ReqOHLCData
                        {
                            Instrument = InputHelper.GetString("Instrument: "),
                            Interval = InputHelper.GetEnum<TimeInterval>("Interval: ")
                        };
                        var OHLCDataInfo = apiClient.MarketDataApi.GetOHLCData(req);
                        Console.WriteLine(JsonConvert.SerializeObject(OHLCDataInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.O:
                    SafeCall(() =>
                    {
                        var req = new ReqOrderBook
                        {
                            Instrument = InputHelper.GetString("Instrument: ")
                        };
                        var orderBookInfo = apiClient.MarketDataApi.GetOrderBook(req);
                        Console.WriteLine(JsonConvert.SerializeObject(orderBookInfo, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }

        #endregion

        #region MarketDataStream
        private bool ShowMarketStreamPage()
        {
            var actions = new Dictionary<ConsoleKey, string>
            {
                [ConsoleKey.A] = "Ticker information on currency pair.",
                [ConsoleKey.B] = "Subscribe on OHLC Data",
                [ConsoleKey.C] = "Subscribe on trade",
                [ConsoleKey.D] = "Subscribe on spread",
                [ConsoleKey.E] = "Subscribe on OrderBook",
                [ConsoleKey.F] = "Unsubscribe all",

                [ConsoleKey.Escape] = "Go back (to main menu)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A:
                    SafeCall(() =>
                    {
                        SubscribeOnTicker();
                    });
                    return true;
                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        SubscribeOnOHLC();
                    });
                    return true;
                case ConsoleKey.C:
                    SafeCall(() =>
                    {
                        SubscribeOnTrade();
                    });
                    return true;
                case ConsoleKey.D:
                    SafeCall(() =>
                    {
                        SubscribeOnSpread();
                    });
                    return true;

                case ConsoleKey.E:
                    SubscribeOrderBook();
                    return true;

                case ConsoleKey.F:
                    UnsubscribeAll();
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }

        private void SubscribeOnTicker()
        {
            string[] instrument = new string[2];
            instrument[0] = InputHelper.GetString("Trade instrument: ");
            instrument[1] = InputHelper.GetString("Trade instrument2: ");
            try
            {
                var subscriptionInfo = apiClient.MarketStreamManager.SubscribeTicker(instrument, OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnOHLC()
        {
            string[] instrument = new string[1];
            instrument[0] = InputHelper.GetString("Trade instrument: ");
            try
            {
                var subscriptionInfo = apiClient.MarketStreamManager.SubscribeOHLC(instrument, OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnTrade()
        {
            string[] instrument = new string[1];
            instrument[0] = InputHelper.GetString("Trade instrument: ");
            try
            {
                var subscriptionInfo = apiClient.MarketStreamManager.SubscribeTrade(instrument, OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnSpread()
        {
            string[] instrument = new string[2];
            instrument[0] = InputHelper.GetString("Trade instrument: ");
            instrument[1] = InputHelper.GetString("Trade instrument: ");
            try
            {
                var subscriptionInfo = apiClient.MarketStreamManager.SubscribeSpread(instrument, OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOrderBook()
        {
            string[] instrument = new string[1];
            instrument[0] = InputHelper.GetString("Trade instrument: ");
            try
            {
                var subscriptionInfo = apiClient.MarketStreamManager.SubscribeBook(instrument, OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UnsubscribeAll()
        {
            try
            {
                apiClient.MarketStreamManager.UnsubscribeAll();

                //Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion


        #region UserDataStream
        private bool SowUserDataStreamPage()
        {
            var actions = new Dictionary<ConsoleKey, string>
            {
                [ConsoleKey.A] = "Subscribe on own trades.",
                [ConsoleKey.B] = "Open orders",
                [ConsoleKey.C] = "Add orders",
                [ConsoleKey.D] = "Cancel order",
                [ConsoleKey.E] = "Unsubscribe all",

                [ConsoleKey.Escape] = "Go back (to main menu)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);
            switch (selectedAction)
            {
                case ConsoleKey.A:
                    SafeCall(() =>
                    {
                        SubscribeOnOwnTrades();
                    });
                    return true;
                case ConsoleKey.B:
                    SafeCall(() =>
                    {
                        AddOrders();
                    });
                    return true;
                case ConsoleKey.C:
                    SafeCall(() =>
                    {

                    });
                    return true;

                case ConsoleKey.E:
                    UnsubscribeAllUserSubscribe();
                    return true;


                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }

        private void SubscribeOnOwnTrades()
        {
            try
            {
                var subscriptionInfo = apiClient.UserDataStream.SubscribeOnOwnTrades(OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AddOrders()
        {
            try
            {
                var subscriptionInfo = apiClient.UserDataStream.AddNewOrder(OnPayloadReceived);

                //Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UnsubscribeAllUserSubscribe()
        {
            try
            {
                apiClient.UserDataStream.UnsubscribeAll();

                //Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        #endregion

        private void OnPayloadReceived<TPayload>(TPayload payload)
        {
            Console.WriteLine(JsonConvert.SerializeObject(payload, Formatting.Indented));
        }

    }
}
