using System;
using NLog;

namespace KrakenApi.Example
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            ICredentialsProvider credentialsProvider = new NppCryptProvider();
            KrakenApiClientCredentials credentials;
            try
            {
                credentials = credentialsProvider.GetCredentials();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            var apiClient = new KrakenApiClient(credentials, logger) { IsDebug = true };

            new ActionManager(apiClient).Run();
        }
    }
}
