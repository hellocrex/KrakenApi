using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    /// <summary>
    /// Default: "both"
    /// Enum: "open" "close" "both"
    /// Which time to use to search
    /// </summary>
    public enum CloseTime
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "both")]
        Both,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "open")]
        Open,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "close")]
        Close
    }
}
