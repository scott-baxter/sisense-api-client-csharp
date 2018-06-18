using JWT.Algorithms;
using JWT.Builder;
using SisenseApiClient.Authenticators.Types;
using SisenseApiClient.Utils.SystemClock;
using System.Threading.Tasks;

namespace SisenseApiClient.Authenticators
{
    public class GlobalTokenAuthenticator : IAuthenticator
    {
        private readonly string _email;
        private readonly string _password;
        private readonly string _apiKey;
        private readonly ISystemClock _systemClock;

        public string ServerUrl { get; set; }

        public TokenType TokenType => TokenType.GlobalToken;

        public GlobalTokenAuthenticator(string username, string password, string apiKey, ISystemClock systemClock)
        {
            _email = username;
            _password = password;
            _apiKey = apiKey;
            _systemClock = systemClock;
        }

        public GlobalTokenAuthenticator(string username, string password, string apiKey)
            : this(username, password, apiKey, new SystemClock())
        {
        }

        public Task<string> GetTokenAsync()
        {
            /*
                JWT token format:
                {
                  "typ": "JWT",
                  "alg": "HS256"
                }.{
                  "iat": 1514764800,
                  "email": "test@email.com",
                  "password": "postit"
                }.[Signature]
            */

            var token = new JwtBuilder()
              .WithAlgorithm(new HMACSHA256Algorithm())
              .WithSecret(_apiKey)
              .AddClaim("iat", _systemClock.UtcNow().ToUnixTimeSeconds())
              .AddClaim("email", _email)
              .AddClaim("password", _password)
              .Build();

            return Task.FromResult(token);
        }
    }
}
