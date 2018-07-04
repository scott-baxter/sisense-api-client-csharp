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
    public class GetElastiCubePermissionsAsyncTests
    {
        [Fact]
        public async Task WhenGettingThePermissions_ShouldReturnThemWithTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetElastiCubePermissionsAsync("myserver", "mycube");

            // Assert
            Assert.Equal("7cf5ab680a32e20ae046c899", result.Id);
            Assert.Equal("myserver", result.Server);
            Assert.Equal("mycube", result.Title);
            Assert.Equal("56a2c7343eb8fa3410000001", result.Creator);
            Assert.Equal(2, result.Shares.Count());
            Assert.Equal("5422c7343cd8fa3410000009", result.Shares.First().PartyId);
            Assert.Equal("user", result.Shares.First().Type);
            Assert.Equal("w", result.Shares.First().Permission);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"{
                        ""_id"": ""7cf5ab680a32e20ae046c899"",
                        ""server"": ""myserver"",
                        ""title"": ""mycube"",
                        ""shares"": [
                            {
                                ""partyId"": ""5422c7343cd8fa3410000009"",
                                ""type"": ""user"",
                                ""permission"": ""w""
                            },
                            {
                                ""partyId"": ""5422c7343cd8fa3410000005"",
                                ""type"": ""group"",
                                ""permission"": ""r""
                            }
                        ],
                        ""creator"": ""56a2c7343eb8fa3410000001""
                    }")
            };
        }
    }
}
