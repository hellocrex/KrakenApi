namespace KrakenApi.Example
{
    interface ICredentialsProvider
    {
        KrakenApiClientCredentials GetCredentials();
    }
}
