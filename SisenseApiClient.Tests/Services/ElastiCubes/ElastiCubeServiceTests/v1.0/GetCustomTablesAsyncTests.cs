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
    public class GetCustomTablesAsyncTests
    {
        [Fact]
        public async Task WhenGettingCustomTables_ShouldReturnThemWithTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetCustomTablesAsync("localhost", "mycube");

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("SELECT id, name \nFROM [users]", result.First().QueryString);
            Assert.Equal("users", result.First().TableName);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"{
                            ""tables"": [
                                {
                                    ""queryString"": ""SELECT id, name \nFROM [users]"",
                                    ""tableName"": ""users""
                                },
                                {
                                    ""queryString"": ""SELECT id, name, price, type \nFROM products\nWHERE id IN (2,7)"",
                                    ""tableName"": ""products""
                                }
                            ]
                        }")
            };
        }
    }
}
