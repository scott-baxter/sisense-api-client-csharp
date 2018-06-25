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
    public class GetServersElastiCubesAsyncTests
    {
        [Fact]
        public async Task WhenGettingTheServersCubes_ShouldReturnTheCorrectInformation()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetServersElastiCubesAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("127.0.0.1", result.First().Address);
            Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1434368184275), result.First().Cubes.First().CreatedUtc);
            Assert.Equal("clients", result.First().Cubes.First().DatabaseName);
            Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1528482614360), result.First().Cubes.First().LastBuiltUtc);
            Assert.Equal(1, result.First().Cubes.First().PermissionsSummary);
            Assert.Equal(87509.8537, result.First().Cubes.First().SizeInMb);
            Assert.Equal(ElastiCubeStatus.Running, result.First().Cubes.First().Status);
            Assert.Equal("mycube", result.First().Cubes.First().Title);
            Assert.Equal(3, result.First().PermissionsSummary);
        }

        [Fact]
        public async Task WhenThereIsAnErrorInAServer_ShouldReturnTheErrorMessage()
        {
            // Arrange
            IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponseWithError());
            IAuthenticator authenticator = new FakeLoginAuthenticator();
            var service = new ElastiCubesService("", httpClient, authenticator);

            // Act
            var result = await service.GetServersElastiCubesAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("LocalHost", result.First().Address);
            Assert.Equal("Unknown error occurred while attempting to communicate with the server", result.First().ErrorMessage);
        }

        private HttpResponseMessage CreateResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"[
                            {
                                ""address"": ""127.0.0.1"",
                                ""cubes"": [
                                    {
                                        ""createdUtc"": ""/Date(1434368184275)/"",
                                        ""databaseName"": ""clients"",
                                        ""lastBuiltUtc"": ""/Date(1528482614360)/"",
                                        ""permissionsSummary"": 1,
                                        ""sizeInMb"": 87509.8537,
                                        ""status"": 2,
                                        ""title"": ""mycube""
                                    }
                                ],
                                ""permissionsSummary"": 3
                            },
                            {
                                ""address"": ""127.0.0.2"",
                                ""cubes"": [
                                    {
                                        ""createdUtc"": ""/Date(1434368184256)/"",
                                        ""databaseName"": ""clients"",
                                        ""lastBuiltUtc"": ""/Date(1528482614369)/"",
                                        ""permissionsSummary"": 5,
                                        ""sizeInMb"": 123.456,
                                        ""status"": 4,
                                        ""title"": ""mycube2""
                                    }
                                ],
                                ""permissionsSummary"": 2
                            }
                        ]")
            };
        }

        private HttpResponseMessage CreateResponseWithError()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    @"[
                            {
                                ""address"": ""LocalHost"",
                                ""errorMessage"": ""Unknown error occurred while attempting to communicate with the server""
                            },
                            {
                                ""address"": ""127.0.0.1"",
                                ""cubes"": [
                                    {
                                        ""createdUtc"": ""/Date(1434368184275)/"",
                                        ""databaseName"": ""clients"",
                                        ""lastBuiltUtc"": ""/Date(1528482614360)/"",
                                        ""permissionsSummary"": 1,
                                        ""sizeInMb"": 87509.8537,
                                        ""status"": 2,
                                        ""title"": ""mycube""
                                    }
                                ],
                                ""permissionsSummary"": 3
                            }
                        ]")
            };
        }
    }
}
