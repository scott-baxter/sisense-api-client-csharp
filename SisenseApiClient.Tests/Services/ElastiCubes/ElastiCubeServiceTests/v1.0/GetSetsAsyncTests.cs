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
    public class GetSetsAsyncTests
    {
        [Fact]
        public async Task WhenGettingAllSets_ShouldReturnThemWithTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetSetsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("693da0c2fa54e82c0d000789", result.First().Id);
            Assert.Equal("127.0.0.1/cube", result.First().FullNames.First());
            Assert.Equal("Majority", result.First().RoutingMode);
            Assert.Equal("My Set", result.First().Title);
            Assert.Equal("1232c7345cd8fa3410000001", result.First().Creator);
            Assert.Equal("1232c7345cd8fa3410000001", result.First().Shares.First().PartyId);
            Assert.Equal("user", result.First().Shares.First().Type);
            Assert.Equal("w", result.First().Shares.First().Permission);
            Assert.Equal("None", result.First().Failover);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"[
                        {
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
                        },
	                    {
                            ""_id"": ""693da0c2fa54e82c0d000790"",
                            ""fullNames"": [
                                ""127.0.0.1/cube2""
                            ],
                            ""routingMode"": ""Majority"",
                            ""title"": ""My Set 2"",
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
                        }
                    ]")
            };
        }
    }
}
