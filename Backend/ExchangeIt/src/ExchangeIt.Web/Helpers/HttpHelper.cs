using ExchangeIt.Web.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeIt.Web.Helpers
{
    public class HttpHelper : IDisposable
    {
        #region private members && constructor
        private HttpClient _httpClient = new HttpClient();
        private bool disposedValue;
        private readonly string _uri;

        public HttpHelper(string URI)
        {
            _uri = URI;
            _httpClient.DefaultRequestHeaders.Add("correlationid", Guid.NewGuid().ToString());
        }
        public HttpHelper(string URI, AuthenticationHeaderValue authenticationHeaderValue) : this(URI)
        {
            _httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
        }
        public HttpHelper(string URI, string clientId, string secretId) : this(URI)
        {
            _httpClient.DefaultRequestHeaders.Add("client_id", clientId);
            _httpClient.DefaultRequestHeaders.Add("client_secret", secretId);
        }
        #endregion
        #region public members
        public async Task<ReturnType> Get<ReturnType>()
        {
            var response = await _httpClient.GetAsync(_uri).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ReturnType>().ConfigureAwait(false);
            }
            var data = JsonConvert.SerializeObject(new
            {
                response = response,
                uri = _uri,
            });

            Log.Debug("Did not get a successful response from {@Uri} - Response = {@Response} :", _uri, response);

            return default(ReturnType);
        }

        public async Task<TokenResponse> Send<ReturnType>(string baseURL, TokenRequest modelforToken)
        {
            var responseFromHubspotAPI = new TokenResponse();

            var requestContent = JsonConvert.SerializeObject(modelforToken,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                            });

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseURL)
            {
                Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            Log.Information("Request received: {@requestMessage}", requestMessage);

            using var response = await client.SendAsync(requestMessage, CancellationToken.None).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                responseFromHubspotAPI = JsonConvert.DeserializeObject<TokenResponse>(responseString);
            }

            return responseFromHubspotAPI;
        }

        public async Task<MailResponse> CreateMail<ReturnType>(string baseURL, string accessToken)
        {
            var responseFromHubspotAPI = new MailResponse();

            var requestContent = JsonConvert.SerializeObject(accessToken,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                            });

            var httpClient = HttpClientFactory.Create();

            httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseURL)
            {
                Content = new StringContent(requestContent, Encoding.UTF8, "application/json")
            };

            Log.Information("Request received: {@requestMessage}", requestMessage);
            using var response = await httpClient.SendAsync(requestMessage, CancellationToken.None).ConfigureAwait(false);


            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                responseFromHubspotAPI = JsonConvert.DeserializeObject<MailResponse>(responseString);
            }

            return responseFromHubspotAPI;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
