using NLog;
using PoissonSoft.KrakenApi.MarketData;
using PoissonSoft.KrakenApi.Transport;
using PoissonSoft.KrakenApi.Userdata;

namespace KrakenApi
{
    public sealed class KrakenApiClient
    {
        private readonly KrakenApiClientCredentials credentials;
        internal static string Endpoint = "https://api.kraken.com";
        internal ILogger Logger { get; }

        /// <summary>
        /// Создание экземпляра
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="logger"></param>
        public KrakenApiClient(KrakenApiClientCredentials credentials, ILogger logger)
        {
            Logger = logger;
            this.credentials = credentials;
            Throttler = new Throttler(this);

            marketDataApi = new MarketDataApi(this, credentials, logger);
            userDataApi = new UserDataApi(this, credentials, logger);
            //spotDataStream = new SpotUserDataStream(this, credentials);
            /* spotDataCollector = new SpotDataCollector(this);
             marketDataApi = new MarketDataApi(this, credentials, logger);
             spotAccountApi = new SpotAccountApi(this, credentials, logger);
             walletApi = new WalletApi(this, credentials, logger);

             marketStreamsManager = new MarketStreamsManager(this, credentials);*/
        }

        /// <summary>
        /// Rest-API для получение рыночных данных
        /// </summary>
        public IMarketDataApi MarketDataApi => marketDataApi;
        private readonly MarketDataApi marketDataApi;

        /// <summary>
        /// Rest-API для получение данных о пользователе
        /// </summary>
        public IUserDataApi UserDataApi => userDataApi;
        private readonly UserDataApi userDataApi;

        /// <summary>
        /// В режиме отладке логгируется больше событий
        /// </summary>
        public bool IsDebug { get; set; } = false;

        internal Throttler Throttler { get; }

    }
}
