using SisenseApiClient.Services.Authentication;
using SisenseApiClient.Services.Authentication.Models;
using SisenseApiClient.Authenticators.Types;
using SisenseApiClient.Utils.HttpClient;
using System;
using System.Threading;
using System.Threading.Tasks;
using SisenseApiClient.Utils.SystemClock;

namespace SisenseApiClient.Authenticators
{
    public class LoginAuthenticator : IAuthenticator
    {
        private const int SECONDS_IN_HOUR = 3600;

        private readonly string _username;
        private readonly string _password;
        private readonly int _tokenExpirationSeconds;
        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);
        private readonly IHttpClient _httpClient;
        private readonly ISystemClock _systemClock;
        private string _bearerToken;
        private DateTimeOffset _tokenExpiration = new DateTimeOffset();

        public string ServerUrl { get; set; }

        public TokenType TokenType => TokenType.Bearer;

        public LoginAuthenticator(string username, string password, IHttpClient httpClient, ISystemClock systemClock,
            int tokenExpirationSeconds = SECONDS_IN_HOUR)
        {
            _username = username;
            _password = password;
            _httpClient = httpClient;
            _systemClock = systemClock;
            _tokenExpirationSeconds = tokenExpirationSeconds;
        }

        public LoginAuthenticator(string username, string password, int tokenExpirationSeconds = SECONDS_IN_HOUR)
            : this(username, password, new HttpClient(), new SystemClock(), tokenExpirationSeconds)
        {
        }

        public async Task<string> GetTokenAsync()
        {
            await _mutex.WaitAsync()
                .ConfigureAwait(false);

            try
            {
                if (_tokenExpiration > _systemClock.UtcNow())
                {
                    return _bearerToken;
                }

                var credentials = new LoginCredentials
                {
                    Username = _username,
                    Password = _password
                };

                var authenticationService = new AuthenticationService(ServerUrl, _httpClient, null);
                var loginResponse = await authenticationService.LoginAsync(credentials)
                    .ConfigureAwait(false);

                _bearerToken = loginResponse.AccessToken;

                // The token returned by sisense does not indicate any expiration time,
                // so here we set some seconds to renew it at some time
                _tokenExpiration = _systemClock.UtcNow().AddSeconds(_tokenExpirationSeconds);

                return _bearerToken;
            }
            finally
            {
                _mutex.Release();
            }
        }
    }
}
