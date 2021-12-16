using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    /// <summary>
    /// Status of order
    /// Enum: "pending" "open" "closed" "canceled" "expired"
    /// Which time to use to search
    /// </summary>
    public enum CloseOrderStatus
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "limit ")]
        Limit,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "stop-loss")]
        StopLoss,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "take-profit")]
        TakeProfit,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "stop-loss-limit ")]
        StopLossLimit,

        /// <summary>
        /// order expired
        /// </summary>
        [EnumMember(Value = "take-profit-limit")]
        TakeProfitLimit
    }
}
