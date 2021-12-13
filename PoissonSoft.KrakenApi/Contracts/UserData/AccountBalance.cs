using System;
using System.Collections.Generic;
using System.Text;

namespace PoissonSoft.KrakenApi.Contracts.UserData
{
    public class AccountBalance
    {
        public object[] error { get; set; }
        public Dictionary<string, string> result { get; set; }
    }

}
