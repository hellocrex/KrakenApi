using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    /// <summary>
    /// Единицы измерения временного интервала для лимита
    /// </summary>
    public enum RestRateLimit
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown = 0,

        /// <summary/>
        [EnumMember(Value = "Starter")]
        Starter = 15,

        /// <summary/>
        [EnumMember(Value = "Intermediate")]
        Intermediate = 20,

        /// <summary/>
        [EnumMember(Value = "Pro")]
        Pro = 20
    }


    /// <summary>
    /// Единицы измерения временного интервала для лимита
    /// </summary>
    public enum SpeedResetRestRateLimit
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown = 0,

        /// <summary/>
        [EnumMember(Value = "Starter")]
        Starter = 1/3,

        /// <summary/>
        [EnumMember(Value = "Intermediate")]
        Intermediate = 1/5,

        /// <summary/>
        [EnumMember(Value = "Pro")]
        Pro = 1
    }
}