using Moq;
using SisenseApiClient.Authenticators;
using SisenseApiClient.Utils.SystemClock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SisenseApiClient.Tests.Authenticators
{
    public class GlobalTokenAthenticatorTests
    {
        [Theory]
        [InlineData("demo@email.com", "John Doe", "secret", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1MTQ3NjQ4MDAsImVtYWlsIjoiZGVtb0BlbWFpbC5jb20iLCJwYXNzd29yZCI6IkpvaG4gRG9lIn0.pagZxUt-upz29-d_76iL4T-A6zTTmC2AAAqFMcEItM0")]
        [InlineData("test@email.com", "postit", "verysecret", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1MTQ3NjQ4MDAsImVtYWlsIjoidGVzdEBlbWFpbC5jb20iLCJwYXNzd29yZCI6InBvc3RpdCJ9.5wY7km4PM0tHNTxe2UprCndL34mukBEtF0PMgLvYxMg")]
        public async Task GetTokenAsync_ShouldReturnAValidToken(string email, string password, string apiKey, string expected)
        {
            // Arrange
            var systemClockMock = new Mock<ISystemClock>();
            systemClockMock
                .Setup(x => x.UtcNow())
                .Returns(new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero));

            var globalTokenAuthenticator = new GlobalTokenAuthenticator(email, password, apiKey, systemClockMock.Object);

            // Act
            string actual = await globalTokenAuthenticator.GetTokenAsync();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
