using Newtonsoft.Json;
using SisenseApiClient.Authenticators;
using SisenseApiClient.Authenticators.Types;
using SisenseApiClient.Exceptions;
using SisenseApiClient.Utils;
using SisenseApiClient.Utils.HttpClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SisenseApiClient.Services
{
    public abstract class ServiceBase
    {
        private readonly string _serverUrl;
        private readonly IHttpClient _httpClient;
        private readonly IAuthenticator _authenticator;

        public ServiceBase(string serverUrl, IHttpClient httpClient, IAuthenticator authenticator)
        {
            _serverUrl = serverUrl;
            _httpClient = httpClient;
            _authenticator = authenticator;
        }

        protected async Task<T> SendRequestAsync<T>(HttpMethod method, string relativeUri, object content = null, bool useAuthentication = true)
            where T : class
        {
            return await SendRequestAsync<T>(method, relativeUri, CancellationToken.None, content, useAuthentication)
                .ConfigureAwait(false);
        }

        protected async Task<T> SendRequestAsync<T>(HttpMethod method, string relativeUri, CancellationToken cancellationToken, object content = null, bool useAuthentication = true)
            where T : class
        {
            string contentBody = await SendRequestAsync(method, relativeUri, cancellationToken, content, useAuthentication)
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(contentBody, JsonUtils.DeserializerSettings);
        }

        protected async Task<string> SendRequestAsync(HttpMethod method, string relativeUri, object content = null)
        {
            return await SendRequestAsync(method, relativeUri, CancellationToken.None, content)
                .ConfigureAwait(false);
        }

        protected async Task<string> SendRequestAsync(HttpMethod method, string relativeUri, CancellationToken cancellationToken, object content = null, bool useAuthentication = true)
        {
            HttpRequestMessage request = await CreateRequestMessageAsync(method, relativeUri, content, useAuthentication)
                .ConfigureAwait(false);

            HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken)
                 .ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound &&
                request.Method == HttpMethod.Get)
            {
                return string.Empty;
            }

            string contentBody = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return contentBody;
            }

            var exception = new SisenseClientHttpException()
            {
                SatusCode = response.StatusCode,
                RequestMessage = request,
                ResponseMessage = response,
                ResponseContent = contentBody
            };

            throw exception;
        }

        private async Task<HttpRequestMessage> CreateRequestMessageAsync(HttpMethod method, string relativeUri, object content = null, bool useAuthentication = true)
        {
            var request = new HttpRequestMessage(method, $"{_serverUrl}/api/{relativeUri}");
            request.Headers.Add("Accept", "application/json");

            if (useAuthentication)
            {
                await AddAuthorizationAsync(request)
                    .ConfigureAwait(false);
            }

            if (content != null)
            {
                string serializedContent = JsonConvert.SerializeObject(content, Formatting.None, JsonUtils.SerializerSettings);
                request.Content = new StringContent(serializedContent);
                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            }

            return request;
        }

        private async Task AddAuthorizationAsync(HttpRequestMessage httpRequestMessage)
        {
            string token = await _authenticator.GetTokenAsync()
                .ConfigureAwait(false);

            if (_authenticator.TokenType == TokenType.Bearer)
            {
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else if (_authenticator.TokenType == TokenType.GlobalToken)
            {
                httpRequestMessage.Headers.Add("X-Api-Key", token);
            }
        }
    }
}
