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
    public class GetBuildRevisionAsyncTests
    {
        [Fact]
        public async Task WhenGettingABuildRevision_ShouldReturnTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetBuildRevisionAsync("localhost", "mycube");

            // Assert
            Assert.Equal("fece84e9-dd78-4ac8-d8b7-fac83fad9a6d", result);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("fece84e9-dd78-4ac8-d8b7-fac83fad9a6d")
            };
        }
    }
}
