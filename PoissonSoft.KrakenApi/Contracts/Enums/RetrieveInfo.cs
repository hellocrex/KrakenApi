using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    public enum RetrieveInfo
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "info")]
        Info,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "leverage")]
        Leverage,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "fees")]
        Fees,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "margin")]
        Margin,
    }
}
