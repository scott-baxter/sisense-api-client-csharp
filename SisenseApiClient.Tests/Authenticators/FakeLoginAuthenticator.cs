using SisenseApiClient.Authenticators;
using SisenseApiClient.Authenticators.Types;
using System.Threading.Tasks;

namespace SisenseApiClient.Tests.Authenticators
{
    class FakeLoginAuthenticator : IAuthenticator
    {
        public string ServerUrl { get; set; }

        public TokenType TokenType => TokenType.Bearer;

        public Task<string> GetTokenAsync()
        {
            return Task.FromResult("abc");
        }
    }
}
