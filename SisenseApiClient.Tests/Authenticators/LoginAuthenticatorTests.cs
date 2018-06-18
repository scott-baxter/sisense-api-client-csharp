using Moq;
using SisenseApiClient.Authenticators;
using SisenseApiClient.Utils.HttpClient;
using SisenseApiClient.Utils.SystemClock;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SisenseApiClient.Tests.Authenticators
{
    public class LoginAuthenticatorTests
    {
        [Fact]
        public async Task GetTokenAsync_ShouldReturnTheAccessToken()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClient>();
            httpClientMock
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .Returns((HttpRequestMessage req, CancellationToken ct) => Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    RequestMessage = req,
                    Content = new StringContent(
                        @"{
                            ""success"": ""true"",
                            ""access_token"": ""abcde""
                        }")
                }));

            var systemClockMock = new Mock<ISystemClock>();
            systemClockMock
                .Setup(x => x.UtcNow())
                .Returns(new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero));

            var authenticator = new LoginAuthenticator("username", "mypass", httpClientMock.Object, 
                systemClockMock.Object);

            // Act
            string actual = await authenticator.GetTokenAsync();

            // Assert
            Assert.Equal("abcde", actual);
        }
    }
}
