using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.Authentication.Models;
using SisenseApiClient.Utils.HttpClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace SisenseApiClient.Services.Authentication
{
    public class AuthenticationService : ServiceBase
    {
        public AuthenticationService(string serverUrl, IHttpClient httpClient, IAuthenticator authenticator) 
            : base(serverUrl, httpClient, authenticator)
        {
        }

        public async Task<LoginResponse> LoginAsync(LoginCredentials credentials)
        {
            return await SendRequestAsync<LoginResponse>(HttpMethod.Post, "v1/authentication/login", credentials, false)
                .ConfigureAwait(false);
        }
    }
}
