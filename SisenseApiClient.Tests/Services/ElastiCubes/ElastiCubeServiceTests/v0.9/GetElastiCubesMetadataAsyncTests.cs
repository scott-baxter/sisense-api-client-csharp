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
    public class GetElastiCubesMetadataAsyncTests
    {
        [Fact]
        public async Task WhenGettingTheMetadata_ShouldReturnThemWithTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetElastiCubesMetadataAsync();

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal("mycube1", result.First().Title);
            Assert.Equal("127.0.0.1/mycube1", result.First().FullName);
            Assert.Equal("a10LgAa0LgAa20LgAa9_amycube1", result.First().Id);
            Assert.Equal("amycube1", result.First().Database);
            Assert.Equal("My Cube Set", result.First().ElastiCubeSet);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"[
	                        {
                                ""title"": ""mycube1"",
                                ""fullname"": ""127.0.0.1/mycube1"",
                                ""id"": ""a10LgAa0LgAa20LgAa9_amycube1"",
                                ""address"": ""127.0.0.1"",
                                ""database"": ""amycube1"",
                                ""elasticubeset"": ""My Cube Set""
                            },
                            {
                                ""title"": ""My Cube Set"",
                                ""fullname"": ""Set/My Cube Set""
                            },
                            {
                                ""title"": ""mycube2"",
                                ""fullname"": ""LocalHost/mycube2"",
                                ""id"": ""aLOCALHOST_aMYCUBE2"",
                                ""address"": ""LocalHost"",
                                ""database"": ""mycube2""
                            }
                        ]")
            };
        }
    }
}
