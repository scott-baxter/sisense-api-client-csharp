using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.ElastiCubes;
using SisenseApiClient.Services.ElastiCubes.Types;
using SisenseApiClient.Tests.Authenticators;
using SisenseApiClient.Tests.Utils.HttpClient;
using SisenseApiClient.Utils.HttpClient;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SisenseApiClient.Tests.Services.ElastiCubes.ElastiCubeServiceTests.v0_9
{
    public class GetElastiCubesByServerAsyncTests
    {
        [Fact]
        public async Task WhenGettingTheServerElasticubes_ShouldReturnThemWithTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetElastiCubesByServerAsync("myserver");

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("9ef5aa670a32e20ae046c83f", result.First().Id);
            Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1491905885118), result.First().CreatedUtc);
            Assert.Equal("clients", result.First().DatabaseName);
            Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1525312805480), result.First().LastBuiltUtc);
            Assert.Equal(1, result.First().PermissionsSummary);
            Assert.Equal(2.3436, result.First().SizeInMb);
            Assert.Equal(ElastiCubeStatus.Stopped, result.First().Status);
            Assert.Equal("clients", result.First().Title);
            Assert.Null(result.First().Error);

            Assert.False(result.First().Shares.First().BelongsToEveryoneGroup);
            Assert.Equal("5422c7343cd8fa3410000001", result.First().Shares.First().PartyId);
            Assert.Equal("w", result.First().Shares.First().Permission);
            Assert.Equal("user", result.First().Shares.First().Type);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"[
                        {
                            ""_id"": ""9ef5aa670a32e20ae046c83f"",
                            ""createdUtc"": ""/Date(1491905885118)/"",
                            ""databaseName"": ""clients"",
                            ""lastBuiltUtc"": ""/Date(1525312805480)/"",
                            ""permissionsSummary"": 1,
                            ""shares"": [
                                {
                                    ""belongsToEveryoneGroup"": false,
                                    ""partyId"": ""5422c7343cd8fa3410000001"",
                                    ""permission"": ""w"",
                                    ""type"": ""user""
                                },
                                {
                                    ""belongsToEveryoneGroup"": true,
                                    ""partyId"": ""5422c7343cd8fa3410000002"",
                                    ""permission"": ""r"",
                                    ""type"": ""group""
                                }
                            ],
                            ""sizeInMb"": 2.3436,
                            ""status"": 1,
                            ""title"": ""clients""
                        },
                        {
                            ""_id"": ""7ff5ab680a32e99ae046c21f"",
                            ""createdUtc"": ""/Date(1515603608635)/"",
                            ""databaseName"": ""products"",
                            ""lastBuiltUtc"": ""/Date(1529867670643)/"",
                            ""permissionsSummary"": 3,
                            ""shares"": [
                                {
                                    ""belongsToEveryoneGroup"": false,
                                    ""partyId"": ""5422c7343cd8fa3410000001"",
                                    ""permission"": ""w"",
                                    ""type"": ""user""
                                },
                                {
                                    ""belongsToEveryoneGroup"": true,
                                    ""partyId"": ""5422c7343cd8fa3410000002"",
                                    ""permission"": ""r"",
                                    ""type"": ""group""
                                }
                            ],
                            ""sizeInMb"": 7853.3032,
                            ""status"": 2,
                            ""title"": ""clients""
                        }
                    ]")
            };
        }
    }
}
