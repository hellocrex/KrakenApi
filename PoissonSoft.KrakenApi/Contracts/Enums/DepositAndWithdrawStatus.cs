using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    /// <summary>
    /// Status of withdraw or deposit operation
    /// </summary>
    public enum DepositAndWithdrawStatus
    {
        /// <summary>
        /// Неизвестный (ошибочный) статус
        /// </summary>
        Unknown,

        /// <summary>
        /// Covers all of the scenarios described above
        /// </summary>
        [EnumMember(Value = "Initial")]
        Initial,

        /// <summary>
        /// Subsequent to execution, but prior to the completion of
        /// settlement-related processes.
        /// </summary>
        [EnumMember(Value = "Pending")]
        Pending,

        /// <summary>
        /// After settlement, but prior to asbolute completion.  For
        /// instance, the period for which a settled transaction remains
        /// exposed to third party cancellation or refund/reversal procedures.
        /// </summary>
        [EnumMember(Value = "Settled")]
        Settled,

        /// <summary>
        /// The transaction completed successfully.
        /// </summary>
        [EnumMember(Value = "Success")]
        Success,

        /// <summary>
        /// The transaction did not complete successfully.
        /// </summary>
        [EnumMember(Value = "Failure")]
        Failure
    }
}
