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
    public class GetCustomTableAsyncTests
    {
        [Fact]
        public async Task WhenGettingACustomTable_ShouldReturnItWithTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetCustomTableAsync("localhost", "mycube", "mytable");

            // Assert
            Assert.Equal("SELECT id, name \nFROM [users]", result.QueryString);
            Assert.Equal("users", result.TableName);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"{
                            ""table"": {
                                    ""queryString"": ""SELECT id, name \nFROM [users]"",
                                    ""tableName"": ""users""
                              }
                        }")
            };
        }
    }
}
