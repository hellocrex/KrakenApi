using System.Runtime.Serialization;

namespace PoissonSoft.KrakenApi.Contracts.Enums
{
    public enum TimeInForce
    {
        /// <summary>
        /// Unknown (erroneous) type
        /// </summary>
        Unknown,

        /// <summary>
        /// GTC (Good-'til-cancelled) is default if the parameter is omitted
        /// </summary>
        [EnumMember(Value = "GTC")]
        GTC,

        /// <summary>
        /// IOC (immediate-or-cancel) will immediately execute the amount possible and cancel any remaining balance rather than resting in the book
        /// </summary>
        [EnumMember(Value = "IOC")]
        IOC,

        /// <summary>
        /// GTD (good-'til-date), if specified, must coincide with a desired expiretm
        /// </summary>
        [EnumMember(Value = "GTD")]
        GTD
    }
}
