using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.ElastiCubes;
using SisenseApiClient.Services.ElastiCubes.Types;
using SisenseApiClient.Tests.Authenticators;
using SisenseApiClient.Tests.Utils.HttpClient;
using SisenseApiClient.Utils.HttpClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SisenseApiClient.Tests.Services.ElastiCubes.ElastiCubeServiceTests.v0_9
{
    public class GetServerStatusAsyncTests
    {
        [Fact]
        public async Task WhenGettingTheServersStatus_ShouldReturnThemWithTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetServerStatusAsync("myserver");

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(ElastiCubeStatus.Stopped, result.First().Status);
            Assert.Equal("clients", result.First().Title);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"[
                        {
                            ""status"": 1,
                            ""title"": ""clients""
                        },
                        {
                            ""status"": 2,
                            ""title"": ""products""
                        }
                    ]")
            };
        }
    }
}
