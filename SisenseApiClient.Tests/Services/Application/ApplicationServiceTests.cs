using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.Application;
using SisenseApiClient.Tests.Authenticators;
using SisenseApiClient.Tests.Utils.HttpClient;
using SisenseApiClient.Utils.HttpClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SisenseApiClient.Tests.Services.Application
{
    public class ApplicationServiceTests
    {
        public class GetStatusAsyncTests
        {
            [Fact]
            public async Task WhenGettingTehStatus_ShouldReturnTheCorrectInformation()
            {
                // Arrange
                IHttpClient httpClient = new FakeHttpClient(responseMessageToReturn: CreateResponse());
                IAuthenticator authenticator = new FakeAuthenticator();
                var service = new ApplicationService("", httpClient, authenticator);

                // Act
                var result = await service.GetStatusAsync();

                // Assert
                Assert.Equal("1.2.3.4", result.Version);
                Assert.True(result.License.IsMobileEnabled);
                Assert.True(result.License.IsExpired);
            }

            private HttpResponseMessage CreateResponse()
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                         @"{
                              ""version"": ""1.2.3.4"",
                              ""license"": {
                                ""isMobileEnabled"": true,
                                ""isExpired"": true
                              }
                          }")
                };
            }
        }
    }
}
