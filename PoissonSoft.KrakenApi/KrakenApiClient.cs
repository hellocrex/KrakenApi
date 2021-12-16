using NLog;
using PoissonSoft.KrakenApi.MarketData;
using PoissonSoft.KrakenApi.Transport;
using PoissonSoft.KrakenApi.Userdata;
using PoissonSoft.KrakenApi.UserFunding;
using PoissonSoft.KrakenApi.UserTrade;

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
            userTradeApi = new UserTradeApi(this, credentials, logger);
            userFundingApi = new UserFundingApi(this, credentials, logger);

            //marketStreamsManager = new MarketStreamsManager(this, credentials);
        }

        /// <summary>
        /// Rest-API для получение рыночных данных
        /// </summary>
        public IMarketDataApi MarketDataApi => marketDataApi;
        private readonly MarketDataApi marketDataApi;

        /// <summary>
        /// Rest-API для получения состояния и данных об операциях пользователя
        /// </summary>
        public IUserDataApi UserDataApi => userDataApi;
        private readonly UserDataApi userDataApi;

        /// <summary>
        /// Rest-API для управления торговыми операциями
        /// </summary>
        public IUserTradeApi UserTradeApi => userTradeApi;
        private readonly UserTradeApi userTradeApi;

        /// <summary>
        /// Rest-API для получения информации о пополнениях и снятиях средств
        /// </summary>
        public IUserFundingApi UserFundingApi => userFundingApi;
        private readonly UserFundingApi userFundingApi;

        /// <summary>
        /// В режиме отладке логгируется больше событий
        /// </summary>
        public bool IsDebug { get; set; } = false;

        internal Throttler Throttler { get; }

    }
}
