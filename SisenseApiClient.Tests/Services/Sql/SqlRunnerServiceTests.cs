using Newtonsoft.Json;
using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.Jaql;
using SisenseApiClient.Services.SqlRunner;
using SisenseApiClient.Tests.Authenticators;
using SisenseApiClient.Tests.Utils.HttpClient;
using SisenseApiClient.Utils.HttpClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SisenseApiClient.Tests.Services.Sql
{
    public class SqlRunnerServiceTests
    {
        public class RunAsyncTests
        {
            [Fact]
            public async Task WhenRunASql_ShouldReturnAString()
            {
                // Arrange
                IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
                IAuthenticator authenticator = new FakeAuthenticator();
                var service = new SqlRunnerService("", httpClient, authenticator);

                // Act
                var result = await service.RunAsync("mycube", "{}");

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
            private class SqlResult
            {
                public IEnumerable<string> Headers { get; set; }

                public IEnumerable<string[]> Values { get; set; }
            }

            [Fact]
            public async Task WhenRunASql_ShouldReturnAnObject()
            {
                // Arrange
                IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
                IAuthenticator authenticator = new FakeAuthenticator();
                var service = new SqlRunnerService("", httpClient, authenticator);

                // Act
                var result = await service.RunAsync<SqlResult>("mycube", "{}");

                // Assert
                Assert.Equal("FirstName", result.Headers.First());
                Assert.Equal("James", result.Values.First()[0]);
                Assert.Equal("Bond", result.Values.First()[1]);
            }

            private HttpResponseMessage CreateResponse()
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        @"{
                        ""headers"": [
                            ""FirstName"",
                            ""LastName"",
                        ],
                        ""values"": [
                            [ ""James"", ""Bond"" ],
                            [ ""Tom"", ""Hanks"" ],
                            [ ""Nicole"", ""Kidman"" ],
                            [ ""Albert"", ""Einstein"" ]
                        ]
                    }")
                };
            }
        }
    }
}
