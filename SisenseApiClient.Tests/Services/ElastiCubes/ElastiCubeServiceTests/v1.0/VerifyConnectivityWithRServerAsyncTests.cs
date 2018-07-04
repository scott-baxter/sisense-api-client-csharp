using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.ElastiCubes;
using SisenseApiClient.Tests.Authenticators;
using SisenseApiClient.Tests.Utils.HttpClient;
using SisenseApiClient.Utils.HttpClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SisenseApiClient.Tests.Services.ElastiCubes.ElastiCubeServiceTests.v1_0
{
    public class VerifyConnectivityWithRServerAsyncTests
    {
        [Fact]
        public async Task WhenThereIsConnectivity_ShouldReturnTrue()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse("true"));
            IAuthenticator authenticator = new FakeAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.VerifyConnectivityWithRServerAsync("localhost", "rserver");

            // Assert
            Assert.True(result.Test);
        }

        [Fact]
        public async Task WhenThereIsNoConnectivity_ShouldReturnFalse()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse("false"));
            IAuthenticator authenticator = new FakeAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.VerifyConnectivityWithRServerAsync("localhost", "rserver");

            // Assert
            Assert.False(result.Test);
        }

        private HttpResponseMessage CreateResponse(string returnValue)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent($"{{ \"test\": {returnValue} }}")
            };
        }
    }
}
