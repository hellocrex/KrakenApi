using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    /// <summary>
    /// Addition status properties 
    /// </summary>
    public enum StatusProp
    {
        /// <summary>
        /// Unknown (erroneous) type
        /// </summary>
        Unknown,

        /// <summary>
        /// A return transaction initiated by Kraken
        /// </summary>
        [EnumMember(Value = "return")]
        Return,

        /// <summary>
        /// Deposit is on hold pending review
        /// </summary>
        [EnumMember(Value = "onhold")]
        Onhold
    }
}
