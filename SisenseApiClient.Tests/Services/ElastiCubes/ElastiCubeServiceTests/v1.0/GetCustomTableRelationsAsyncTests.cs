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
    public class GetCustomTableRelationsAsyncTests
    {
        [Fact]
        public async Task WhenGettingCustomTablesRelations_ShouldReturnThemWithTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetCustomTableRelationsAsync("localhost", "mycube", "mytable");

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal("key", result.First().SourceField);
            Assert.Equal("articlekey", result.First().TargetField);
            Assert.Equal("articles", result.First().TargetTable);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"{
                            ""relations"": [
                                {
                                    ""sourceField"": ""key"",
                                    ""targetField"": ""articlekey"",
                                    ""targetTable"": ""articles""
                                },
                                {
                                    ""sourceField"": ""key"",
                                    ""targetField"": ""categorykey"",
                                    ""targetTable"": ""articlecategories""
                                },
                                {
                                    ""sourceField"": ""key"",
                                    ""targetField"": ""commentkey"",
                                    ""targetTable"": ""commentarticles""
                                }
                            ]
                        }")
            };
        }
    }
}
