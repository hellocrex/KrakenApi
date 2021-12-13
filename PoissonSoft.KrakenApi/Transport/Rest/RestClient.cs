using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KrakenApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NLog;
using PoissonSoft.KrakenApi.Contracts.Exceptions;
using PoissonSoft.KrakenApi.Contracts.Serialization;
using PoissonSoft.KrakenApi.Contracts.UserData.Request;
using PoissonSoft.KrakenApi.Utils;

namespace PoissonSoft.KrakenApi.Transport.Rest
{
    internal sealed class RestClient : IDisposable
    {
        private readonly ILogger logger;
        private readonly Throttler throttler;
        private readonly string userFriendlyName;

        private readonly HttpClient httpClient;
        private readonly bool useSignature;
        private readonly byte[] secretKey;
        private readonly KrakenApiClientCredentials Credentials;

        private readonly JsonSerializerSettings serializerSettings;
        private long nonce;
        public RestClient(ILogger logger, string baseEndpoint, EndpointSecurityType[] securityTypes, KrakenApiClientCredentials credentials, Throttler throttler)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.throttler = throttler ?? throw new ArgumentNullException(nameof(throttler));

            useSignature = securityTypes?.Any(x =>
                x == EndpointSecurityType.Private) ?? false;
            secretKey = Encoding.UTF8.GetBytes(credentials.SecretKey);
            Credentials = credentials;
            userFriendlyName = $"{nameof(RestClient)} ({baseEndpoint})";

            serializerSettings = new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All,
                    new SerializationContext { Logger = logger })
            };

            var httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                Proxy = ProxyHelper.CreateProxy(credentials)
            };
            nonce = 0;

            baseEndpoint = baseEndpoint.Trim();
            if (!baseEndpoint.EndsWith("/")) baseEndpoint += '/';
            httpClient = new HttpClient(httpClientHandler, true)
            {
                Timeout = TimeSpan.FromSeconds(20),
                BaseAddress = new Uri(baseEndpoint),
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }

        /// <summary>
        /// Выполнить запрос
        /// </summary>
        /// <typeparam name="TResp">Тип возвращаемого значения</typeparam>
        /// <returns></returns>
        public TResp MakeRequest<TResp>(RequestParameters requestParameters)
        {
            if (requestParameters.RequestWeight > 0)
                throttler.ThrottleRest(requestParameters.RequestWeight, requestParameters.IsHighPriority, requestParameters.IsOrderRequest);


            void checkResponse(HttpResponseMessage resp, string body)
            {
                throttler.ApplyRestResponseHeaders(resp.Headers);

                if (resp.StatusCode == HttpStatusCode.OK) return;

                string msg;

                // HTTP 429 return code is used when breaking a request rate limit.
                // HTTP 418 return code is used when an IP has been auto-banned for continuing to send requests after receiving 429 codes.
                // A Retry-After header is sent with a 418 or 429 responses and will give the number of seconds required to wait, in the case of a 429,
                // to prevent a ban, or, in the case of a 418, until the ban is over.
                if (resp.StatusCode == (HttpStatusCode)429 || resp.StatusCode == (HttpStatusCode)418)
                {
                    //var retryAfter = resp.Headers.RetryAfter?.Delta;
                    //throttler.StopAllRequestsDueToRateLimit(retryAfter);

                    //msg = $"{userFriendlyName}. Обнаружено превышение лимита запросов. " +
                    //      $"{(int)resp.StatusCode} ({resp.ReasonPhrase}). Retry-After={retryAfter}";
                    //logger.Error(msg);
                    //throw new RequestRateLimitBreakingException(msg);
                }

                msg = $"{userFriendlyName}. На запрос {requestParameters.SpecialPath} от сервера получен код ответа" +
                      $" {(int)resp.StatusCode} ({resp.StatusCode})\nТело ответа:\n{body}";
                logger.Error(msg);
                throw new EndpointCommunicationException(msg);
            }

            string strResp;
            try
            {
                string url;

                if (requestParameters.Method == HttpMethod.Post || requestParameters.Method == HttpMethod.Put)
                {
                    //var postBody = JsonConvert.SerializeObject(requestParameters.Parameters);
                    var endPoint = $"{requestParameters.SpecialPath}";

                    string postUrl = BuildQueryString(requestParameters.Parameters);
                    if (useSignature)
                    {
                        //var postData = postUrl + postBody;
                        //var nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
                        //n = SignHttpWebRequest(endPoint, postData, nonce);
                        //var parameters = new Dictionary<string, object>
                        //{
                        //    //{"nonce", GetNonce(Convert.ToInt64(nonceTime))}
                        //    {"nonce", nonceTime},
                        //   // {"asset", asset}
                        //};

                       // var stringData = string.Join("&", parameters.OrderBy(p => p.Key != "nonce").Select(p => $"{p.Key}={p.Value}"));
                       AddAuthenticationToHeaders($"{httpClient.BaseAddress}{endPoint}?{postUrl}", HttpMethod.Post, requestParameters.Parameters);
                    }
                    
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    using (var content =
                        new StringContent(postUrl, Encoding.UTF8, "application/x-www-form-urlencoded"))
                    { 
                        using (var result = requestParameters.Method == HttpMethod.Post
                            ? httpClient.PostAsync(endPoint, content).Result
                            : httpClient.PutAsync(endPoint, null).Result)
                        {
                            strResp = result.Content.ReadAsStringAsync().Result;
                            checkResponse(result, strResp);
                        }
                    }
                }
                else if (requestParameters.Method == HttpMethod.Get || requestParameters.Method == HttpMethod.Delete)
                {
                    string body = "";

                    if (requestParameters.SpecialBuildPath)
                    {
                        body = requestParameters.Parameters["Parameter"] ?? string.Empty;
                    }

                    if (body != string.Empty)
                    {
                        url = $"{requestParameters.SpecialPath}{($"/{body}")}";
                    }
                    else
                    {
                        string queryString = BuildQueryString(requestParameters.Parameters);
                        url =
                            $"{requestParameters.SpecialPath}{(string.IsNullOrEmpty(queryString) ? string.Empty : $"?{queryString}")}";
                    }

                    // if (useSignature) SignHttpWebRequest(requestParameters.Method.ToString(), url);
                    
                    using (var result = requestParameters.Method == HttpMethod.Get
                        ? httpClient.GetAsync(url).Result
                        : httpClient.DeleteAsync(url).Result)
                    {
                        strResp = result.Content.ReadAsStringAsync().Result;

                        // превышение лимита количества вызовов метода
                        if (result.StatusCode == (HttpStatusCode)429)
                        {
                            //checkResponse(result, strResp);
                        }
                        
                        checkResponse(result, strResp);
                    }
                }
                else
                {
                    throw new Exception($"Unsupporded HTTP-method {requestParameters.Method}");
                }
            }
            catch (EndpointCommunicationException)
            {
                throw;
            }
            catch (HttpRequestException e)
            {
                logger.Error($"{userFriendlyName}. При отправке HTTP-запроса возникло исключение\n{e}");
                throw;
            }
            catch (AggregateException e) when (e.InnerExceptions.Count == 1 &&
                                               e.InnerExceptions[0] is TaskCanceledException)
            {
                var msg = $"{userFriendlyName}. Возникло исключение {nameof(TaskCanceledException)} в связи с истечением таймаута запроса. " +
                          "Возможно, сервер временно не доступен";
                logger.Error(msg);
                throw new TimeoutException(msg, e);
            }
            catch (Exception e)
            {
                logger.Error($"{userFriendlyName}. При запросе данных с сервера возникло исключение\n{e}");
                throw;
            }

            if (string.IsNullOrWhiteSpace(strResp)) return default;
            if (typeof(TResp) == typeof(string)) return (TResp)(object)strResp;

            try
            {
                return JsonConvert.DeserializeObject<TResp>(strResp, serializerSettings);
            }
            catch (Exception e)
            {
                logger.Error($"{userFriendlyName}. Попытка десериализации строки, полученной от сервера вызвала исключение\n" +
                             $"Ответ сервера: {strResp}\n{e}");
                throw;
            }
        }
       
        private string SignHttpWebRequest(string endpoint, string postData, string nonce = "")
        {
            //nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            // Step 1: concatenate postData + endpoint
            var message = postData + nonce + endpoint;

            //Step 2: hash the result of step 1 with SHA256
            var hash256 = new SHA256Managed();
            var hash = hash256.ComputeHash(Encoding.UTF8.GetBytes(message));

            //step 3: base64 decode apiPrivateKey
            var secretDecoded = (System.Convert.FromBase64String(Credentials.SecretKey));

            //step 4: use result of step 3 to hash the resultof step 2 with HMAC-SHA512
            var hmacsha512 = new HMACSHA512(secretDecoded);
            var hash2 = hmacsha512.ComputeHash(hash);

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("APIKey", Credentials.ApiKey);
            httpClient.DefaultRequestHeaders.Add("Authent", System.Convert.ToBase64String(hash2));
            httpClient.DefaultRequestHeaders.Add("Nonce", nonce.ToString());
            httpClient.DefaultRequestHeaders.Add("User-agent", "cf-api-python/1.0");
            //step 5: base64 encode the result of step 4 and return
            return System.Convert.ToBase64String(hash2);
        }


        //public void AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed)
        public void AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, string> parameters)
        {
            if (Credentials.ApiKey == null)
                    throw new ArgumentException("ApiKey/Secret needed");
                
                BuildQueryString(parameters);
                parameters.TryGetValue("nonce", out string nonceTime);
                //var nonceTime = parameters.Single(n => n.Key == "nonce").Value;
               // var paramList = parameters.OrderBy(o => o.Key != "nonce");
                //var pars = string.Join("&", paramList.Select(p => $"{p.Key}={p.Value}"));
                var pars = BuildQueryString(parameters);

                var result = new Dictionary<string, string> { { "API-Key", Credentials.ApiKey } };
                var np = nonceTime + pars;
                byte[] nonceParamsBytes;
                using (var sha = SHA256.Create())
                    nonceParamsBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(np));
                var pathBytes = Encoding.UTF8.GetBytes(uri.Split(new[] { ".com" }, StringSplitOptions.None)[1].Split('?')[0]);
                var allBytes = pathBytes.Concat(nonceParamsBytes).ToArray();

                byte[] sign;
                using (var hmac = new HMACSHA512(Convert.FromBase64String(Credentials.SecretKey)))
                    sign = hmac.ComputeHash(allBytes);

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("API-Key", Credentials.ApiKey);
                httpClient.DefaultRequestHeaders.Add("API-Sign", Convert.ToBase64String(sign));
                httpClient.DefaultRequestHeaders.Add("Nonce", nonceTime.ToString());
                httpClient.DefaultRequestHeaders.Add("User-agent", "cf-api-python/1.0");

        }

        private long GetNonce(long time)
        {
            nonce = (nonce + 1) & 8191;
            return time + nonce;
        }

        private long lastId = Convert.ToInt64(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
        private long GenerateUniqueId()
        {
            return Interlocked.Increment(ref lastId);
        }

        private string BuildQueryString(Dictionary<string, string> paramDic)
        {
            var mainParams = paramDic?.Any() != true
                ? string.Empty
                : string.Join("&", paramDic.Select(x => $"{x.Key}={x.Value}"));

            return mainParams;
        }
        public void Dispose()
        {
            httpClient?.Dispose();
        }

    }

    internal sealed class RequestParameters
    {
        /// <summary>
        /// HTTP-метод
        /// </summary>
        public HttpMethod Method { get; }

        /// <summary>
        /// Особое построение строки запроса
        /// </summary>
        public bool SpecialBuildPath { get; private set; }

        /// <summary>
        /// Путь к ресурсу (без базового адреса эндпоинта)
        /// </summary>
        public string SpecialPath { get; }

        /// <summary>
        /// Максимальное число запросов в минуту
        /// </summary>
        public int RequestWeight { get; }

        /// <summary>
        /// Признак того, что высокого приоритета запроса
        /// </summary>
        public bool IsHighPriority { get; set; }

        /// <summary>
        /// Это запрос на работу с ордерами (размещение/удаление/модификация)
        /// </summary>
        public bool IsOrderRequest { get; set; }

        /// <summary>
        /// Передавать все параметры через QueryString URL
        /// </summary>
        public bool PassAllParametersInQueryString { get; set; }

        /// <summary>
        /// Параметры запроса
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        public RequestParameters(HttpMethod method, string specialPath, int requestWeight = 100, object request = null)
        {
            Method = method;
            SpecialPath = specialPath;
            RequestWeight = requestWeight;
            if (request != null) Parameters = GenerateParametersFromObject(request);
        }

        static RequestParameters()
        {
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
        }

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            FloatParseHandling = FloatParseHandling.Decimal,
            Formatting = Formatting.Indented
        };

        private Dictionary<string, string> GenerateParametersFromObject(object obj)
        {
            if (obj == null) return null;
            try
            {
                //if (obj is SpecialBuildQuery)
                //    SpecialBuildPath = true;

                var jObject = JObject.Parse(JsonConvert.SerializeObject(obj, jsonSerializerSettings));

                return jObject.Children()
                    .Cast<JProperty>()
                    .Where(x => x.Value.Type != JTokenType.Null)
                    .ToDictionary(x => x.Name, x => x.Value.ToString());
            }
            catch
            {
                return null;
            }
        }
    }
}
