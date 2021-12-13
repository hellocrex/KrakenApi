using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KrakenApi.Contracts.Enums;
using PoissonSoft.KrakenApi.Contracts.MarketData;
using PoissonSoft.KrakenApi.Contracts.MarketData.Request;

namespace KrakenApi.Example
{
    internal partial class ActionManager
    {
        private bool ShowMarketDataPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.I] = "Ticker Information",
                [ConsoleKey.B] = "OHCL Data",
                [ConsoleKey.O] = "Order Book",
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

    }
}
