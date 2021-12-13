using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    /// <summary>
    /// Type of order (buy/sell)
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "buy")]
        Buy,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "sell")]
        Sell
    }
}
