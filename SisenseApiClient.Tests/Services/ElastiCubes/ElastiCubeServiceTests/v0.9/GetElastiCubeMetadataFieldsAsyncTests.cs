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
    public class GetElastiCubeMetadataFieldsAsyncTests
    {
        [Fact]
        public async Task WhenGettingTheMetadataFields_ShouldReturnThemWithCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetElastiCubeMetadataFieldsAsync("mycube");

            // Assert
            Assert.Equal(3, result.Count());
            Assert.Equal("[def_daysofweek.day]", result.First().Id);
            Assert.Equal("dimension", result.First().Type);
            Assert.Equal("text", result.First().DimType);
            Assert.Equal("day", result.First().Title);
            Assert.Equal("def_daysofweek", result.First().Table);
            Assert.Equal("day2", result.First().Column);
            Assert.False(result.First().Merged);
            Assert.True(result.First().Indexed);
            Assert.Null(result.First().DimensionTable);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"[
                        {
                            ""id"": ""[def_daysofweek.day]"",
                            ""type"": ""dimension"",
                            ""dimtype"": ""text"",
                            ""title"": ""day"",
                            ""table"": ""def_daysofweek"",
                            ""column"": ""day2"",
                            ""merged"": false,
                            ""indexed"": true
                        },
	                    {
                            ""id"": ""[categorykey]"",
                            ""type"": ""dimension"",
                            ""dimtype"": ""numeric"",
                            ""title"": ""categorykey"",
                            ""dimensionTable"": true
                        },
                        {
                            ""id"": ""[def_usertypes.type]"",
                            ""type"": ""dimension"",
                            ""dimtype"": ""text"",
                            ""title"": ""type"",
                            ""table"": ""def_usertypes"",
                            ""column"": ""type"",
                            ""merged"": false,
                            ""indexed"": true
                        }
                    ]")
            };
        }
    }
}
