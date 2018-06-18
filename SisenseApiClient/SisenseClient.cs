using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.Authentication;
using SisenseApiClient.Services.ElastiCubes;
using SisenseApiClient.Utils.HttpClient;

namespace SisenseApiClient
{
    public class SisenseClient
    {
        public AuthenticationService Authentication { get; }
        public ElastiCubesService ElastiCubes { get; }

        public SisenseClient(string serverUrl, IAuthenticator authenticator, IHttpClient httpClient)
        {
            authenticator.ServerUrl = serverUrl;
            Authentication = new AuthenticationService(serverUrl, httpClient, authenticator);
            ElastiCubes = new ElastiCubesService(serverUrl, httpClient, authenticator);
        }

        public SisenseClient(string serverUrl, IAuthenticator authenticator)
            : this(serverUrl, authenticator, new HttpClient())
        {
        }
    }
}
