using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SisenseApiClient.Utils.HttpClient
{
    public class HttpClient : IHttpClient
    {
        // It's important to not use cookies because that avoid problems when calling the login endpoint twice
        private static readonly System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient(new HttpClientHandler() { UseCookies = false } );

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await _client.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
