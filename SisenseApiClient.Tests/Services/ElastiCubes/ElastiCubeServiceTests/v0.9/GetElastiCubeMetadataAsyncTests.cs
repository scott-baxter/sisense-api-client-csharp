﻿using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.ElastiCubes;
using SisenseApiClient.Tests.Authenticators;
using SisenseApiClient.Tests.Utils.HttpClient;
using SisenseApiClient.Utils.HttpClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SisenseApiClient.Tests.Services.ElastiCubes.ElastiCubeServiceTests.v0_9
{
    public class GetElastiCubeMetadataAsyncTests
    {
        [Fact]
        public async Task WhenGettingTheMetadata_ShouldReturnItWithCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetElastiCubeMetadataAsync("mycube");

            // Assert
            Assert.Equal("mycube1", result.Title);
            Assert.Equal("127.0.0.1/mycube1", result.FullName);
            Assert.Equal("a10LgAa0LgAa20LgAa9_amycube1", result.Id);
            Assert.Equal("amycube1", result.Database);
            Assert.Equal("My Cube Set", result.ElastiCubeSet);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"{
                            ""title"": ""mycube1"",
                            ""fullname"": ""127.0.0.1/mycube1"",
                            ""id"": ""a10LgAa0LgAa20LgAa9_amycube1"",
                            ""address"": ""127.0.0.1"",
                            ""database"": ""amycube1"",
                            ""elasticubeset"": ""My Cube Set""
                        }")
            };
        }
    }
}
