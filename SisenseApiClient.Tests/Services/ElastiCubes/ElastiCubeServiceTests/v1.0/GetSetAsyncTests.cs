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

namespace SisenseApiClient.Tests.Services.ElastiCubes.ElastiCubeServiceTests.v1_0
{
    public class GetSetAsyncTests
    {
        [Fact]
        public async Task WhenGettingASet_ShouldReturnTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetSetAsync("");

            // Assert
            Assert.Equal("693da0c2fa54e82c0d000789", result.Id);
            Assert.Equal("127.0.0.1/cube", result.FullNames.First());
            Assert.Equal("Majority", result.RoutingMode);
            Assert.Equal("My Set", result.Title);
            Assert.Equal("1232c7345cd8fa3410000001", result.Creator);
            Assert.Equal("1232c7345cd8fa3410000001", result.Shares.First().PartyId);
            Assert.Equal("user", result.Shares.First().Type);
            Assert.Equal("w", result.Shares.First().Permission);
            Assert.Equal("None", result.Failover);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"{
                            ""_id"": ""693da0c2fa54e82c0d000789"",
                            ""fullNames"": [
                                ""127.0.0.1/cube""
                            ],
                            ""routingMode"": ""Majority"",
                            ""title"": ""My Set"",
                            ""creator"": ""1232c7345cd8fa3410000001"",
                            ""shares"": [
                                {
                                    ""partyId"": ""1232c7345cd8fa3410000001"",
                                    ""type"": ""user"",
                                    ""permission"": ""w""
                                },
                                {
                                    ""partyId"": ""1232c7345cd8fa3410000002"",
                                    ""type"": ""group"",
                                    ""permission"": ""r""
                                }
                            ],
                            ""failover"": ""None""
                        }")
            };
        }
    }
}
