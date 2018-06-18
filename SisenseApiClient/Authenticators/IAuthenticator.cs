using SisenseApiClient.Authenticators.Types;
using System.Threading.Tasks;

namespace SisenseApiClient.Authenticators
{
    public interface IAuthenticator
    {
        string ServerUrl { get; set; }

        TokenType TokenType { get; }

        Task<string> GetTokenAsync();
    }
}
