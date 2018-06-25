using SisenseApiClient.Utils.HttpClient;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SisenseApiClient.Tests.Utils.HttpClient
{
    class FakeHttpClient : IHttpClient
    {
        public HttpResponseMessage ResponseMessageToReturn { get; set; }

        public FakeHttpClient(HttpResponseMessage responseMessageToReturn)
        {
            ResponseMessageToReturn = responseMessageToReturn;
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(ResponseMessageToReturn);
        }
    }
}
