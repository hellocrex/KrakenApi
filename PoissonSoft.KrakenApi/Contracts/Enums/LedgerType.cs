using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    public enum LedgerType
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "trade")]
        Trade,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "deposit")]
        Deposit,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "withdraw")]
        Withdraw,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "transfer")]
        Transfer,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "margin")]
        Margin,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "rollover")]
        Rollover,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "spend")]
        Spend,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "receive")]
        Receive,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "settled")]
        Settled,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "adjustment")]
        Adjustment
    }
}
