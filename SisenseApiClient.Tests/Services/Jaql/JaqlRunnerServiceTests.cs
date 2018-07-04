using Newtonsoft.Json;
using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.Jaql;
using SisenseApiClient.Tests.Authenticators;
using SisenseApiClient.Tests.Utils.HttpClient;
using SisenseApiClient.Utils.HttpClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SisenseApiClient.Tests.Services.Jaql
{
    public class JaqlRunnerServiceTests
    {
        public class RunAsyncTests
        {
            [Fact]
            public async Task WhenRunAJaql_ShouldReturnAString()
            {
                // Arrange
                IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
                IAuthenticator authenticator = new FakeAuthenticator();
                var service = new JaqlRunnerService("", httpClient, authenticator);

                // Act
                var result = await service.RunAsync("{}");

                // Assert
                Assert.Equal(@"{""title"":""mycube""}", result);
            }

            private HttpResponseMessage CreateResponse()
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{""title"":""mycube""}")
                };
            }
        }

        public class RunAsyncOfTTests
        {
            private class JaqlResult
            {
                public class DataSource
                {
                    public string FullName { get; set; }
                    public string RevisionId { get; set; }
                }

                public class Value
                {
                    public double Data { get; set; }
                    public string Text { get; set; }
                }

                public IEnumerable<string> Headers { get; set; }

                [JsonProperty(PropertyName = "datasource")]
                public DataSource DataSource1 { get; set; }

                public IEnumerable<string> Metadata { get; set; }

                public IEnumerable<Value> Values { get; set; }
            }

            [Fact]
            public async Task WhenRunAJaql_ShouldReturnAnObject()
            {
                // Arrange
                IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
                IAuthenticator authenticator = new FakeAuthenticator();
                var service = new JaqlRunnerService("", httpClient, authenticator);

                // Act
                var result = await service.RunAsync<JaqlResult>("{}");

                // Assert
                Assert.Equal("Average Orders Per Customer", result.Headers.First());
                Assert.Equal("LocalHost/Training", result.DataSource1.FullName);
                Assert.Equal("3fe81e9d1-32bf-4618-902f-c941627f7654", result.DataSource1.RevisionId);
                Assert.Equal(4.744186046511628, result.Values.First().Data);
                Assert.Equal("4.74418604651163", result.Values.First().Text);
            }

            private HttpResponseMessage CreateResponse()
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        @"{
                        ""headers"": [
                            ""Average Orders Per Customer""
                        ],
                        ""datasource"": {
                            ""fullname"": ""LocalHost/Training"",
                            ""revisionId"": ""3fe81e9d1-32bf-4618-902f-c941627f7654""
                        },
                        ""metadata"": [],
                        ""values"": [
                            {
                                ""data"": 4.744186046511628,
                                ""text"": ""4.74418604651163""
                            }
                        ]
                    }")
                };
            }
        }
    }
}
