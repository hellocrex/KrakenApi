using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum TimeInterval
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "1")]
        Minute = 1,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "5")]
        Minute_5 = Minute * 5,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "15")]
        Minute_15 = Minute * 15,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "30")]
        HalfHour = 1,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "60")]
        Hour = Minute * 60,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "240")]
        Hour_4 = Hour * 4,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "1440")]
        Day = Hour * 24,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "10080")]
        Week = Day * 7,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "21600")]
        Day_15 = Day * 15,
    }
}
