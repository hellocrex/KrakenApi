using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    /// <summary>
    /// Status of order
    /// Enum: "pending" "open" "closed" "canceled" "expired"
    /// Which time to use to search
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// order pending book entry
        /// </summary>
        [EnumMember(Value = "pending ")]
        Pending,

        /// <summary>
        /// open order
        /// </summary>
        [EnumMember(Value = "open")]
        Open,

        /// <summary>
        /// closed order
        /// </summary>
        [EnumMember(Value = "closed")]
        Closed,

        /// <summary>
        /// order canceled
        /// </summary>
        [EnumMember(Value = "canceled ")]
        Canceled,

        /// <summary>
        /// order expired
        /// </summary>
        [EnumMember(Value = "expired")]
        Expired
    }
}
