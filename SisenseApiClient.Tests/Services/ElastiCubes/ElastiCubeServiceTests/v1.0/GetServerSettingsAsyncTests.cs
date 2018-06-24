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
    public class GetServerSettingsAsyncTests
    {
        [Fact]
        public async Task WhenGettingTheServerSettings_ShouldReturnTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetServerSettingsAsync("localhost");

            // Assert
            Assert.Equal(100000, result.DataImportChunkSize);
            Assert.Equal("F:\\Sisense\\PrismServer\\ElastiCubeData", result.DefaultServerDataFolder);
            Assert.Equal(100, result.ElasticubeMemoryAllocation);
            Assert.Equal(16, result.ProcessorCount);
            Assert.Equal(300, result.QueryTimeout);
            Assert.True(result.RecycleQueries);
            Assert.Equal("127.0.0.1:6311", result.RServer);
            Assert.False(result.RServerEnabled);
            Assert.Equal(8, result.SimultaneousQueryExecutions);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"{
                            ""DataImportChunkSize"": 100000,
                            ""DefaultServerDataFolder"": ""F:\\Sisense\\PrismServer\\ElastiCubeData"",
                            ""ElasticubeMemoryAllocation"": 100,
                            ""ProcessorCount"": 16,
                            ""QueryTimeout"": 300,
                            ""RecycleQueries"": true,
                            ""Rserver"": ""127.0.0.1:6311"",
                            ""RserverEnabled"": false,
                            ""SimultaneousQueryExecutions"": 8
                        }")
            };
        }
    }
}
