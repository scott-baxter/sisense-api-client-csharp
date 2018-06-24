using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.ElastiCubes;
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
    public class GetServersMetadataAsyncTests
    {
        [Fact]
        public async Task WhenGettingTheServersMetadata_ShouldReturnThemWithTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetServersMetadataAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("192.168.4", result.First().Address);
            Assert.Equal(1, result.First().PermissionsSummary);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"[
                        {
                            ""address"": ""192.168.4"",
                            ""permissionsSummary"": 1
                        },
                        {
                            ""address"": ""LocalHost"",
                            ""permissionsSummary"": 3
                        }
                    ]")
            };
        }
    }
}
