namespace PoissonSoft.KrakenApi.Transport
{
    /// <summary>
    /// Тип безопасности
    /// </summary>
    public enum EndpointSecurityType
    {
        /// <summary>
        /// A valid API-Key and signature not use.
        /// </summary>
        Public,

        /// <summary>
        /// Endpoint requires sending a valid API-Key and signature.
        /// </summary>
        Private
    }
}
