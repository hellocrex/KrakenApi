﻿using System;
using System.Collections.Generic;
using PoissonSoft.CommonUtils.ConsoleUtils;

namespace KrakenApi.Example
{
    internal partial class ActionManager
    {
        private readonly KrakenApiClient apiClient;

        public ActionManager(KrakenApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public void Run()
        {
            while (ShowMainPage()) { }
            Console.WriteLine("> The program stopped. Press any key to exit...");
            Console.ReadKey();
        }

        private bool ShowMainPage()
        {
            Console.Clear();
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Market Data API",
                [ConsoleKey.B] = "User Data",
                [ConsoleKey.C] = "User Trading",
                [ConsoleKey.D] = "User Funding",
                [ConsoleKey.E] = "User Staking",

                [ConsoleKey.Escape] = "Go back (exit)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A:
                    while (ShowMarketDataPage()) { }
                    return true;
                case ConsoleKey.B:
                    while (ShowUserDataPage()) { }
                    return true;
                case ConsoleKey.C:
                    while (ShowUserTradingPage()) { }
                    return true;
                case ConsoleKey.D:
                    while (ShowUserFundingPage()) { }
                    return true;
                case ConsoleKey.E:
                    while (ShowUserStakingPage()) { }
                    return true;

                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }

        private void SafeCall(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}