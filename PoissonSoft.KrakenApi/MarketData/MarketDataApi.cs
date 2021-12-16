using System;
using System.Net.Http;
using KrakenApi;
using NLog;
using PoissonSoft.KrakenApi.Contracts.MarketData;
using PoissonSoft.KrakenApi.Contracts.MarketData.Request;
using PoissonSoft.KrakenApi.Transport;
using PoissonSoft.KrakenApi.Transport.Rest;
using RequestParameters = PoissonSoft.KrakenApi.Transport.Rest.RequestParameters;

namespace PoissonSoft.KrakenApi.MarketData
{
    internal class MarketDataApi: IMarketDataApi, IDisposable
    {
        private readonly KrakenApiClient apiClient;
        private readonly RestClient client;

        public MarketDataApi(KrakenApiClient apiClient, KrakenApiClientCredentials credentials, ILogger logger)
        {
            apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            client = new RestClient(logger, KrakenApiClient.Endpoint,
                new[] { EndpointSecurityType.Public }, credentials, apiClient.Throttler);
            
        }

        #region Market Data
        public InstrumentInformation GetTradableAssetPairs(ReqInstrumentInformation req)
        {
            return client.MakeRequest<InstrumentInformation>(new RequestParameters(HttpMethod.Get, "0/public/AssetPairs", 1, req));
        }

        public TickerInformation GetTickerInformation(ReqInstrumentInformation req)
        {
            return client.MakeRequest<TickerInformation>(new RequestParameters(HttpMethod.Get, "0/public/Ticker", 1, req));
        }

        public OHLCData GetOHLCData(ReqOHLCData req)
        {
            return client.MakeRequest<OHLCData>(new RequestParameters(HttpMethod.Get, "0/public/OHLC", 1, req));
        }

        public OrderBook GetOrderBook(ReqOrderBook req)
        {
            return client.MakeRequest<OrderBook>(new RequestParameters(HttpMethod.Get, "0/public/Depth", 1, req));
        }
        #endregion



        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
