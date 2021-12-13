using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "market")]
        Market,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "limit")]
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
        [EnumMember(Value = "stop-loss-limit")]
        StopLossLimit,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "take-profit-limit")]
        TakeProfitLimit,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "settle-position")]
        SettlePosition

    }
}
