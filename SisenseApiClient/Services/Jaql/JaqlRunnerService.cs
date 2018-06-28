using SisenseApiClient.Authenticators;
using SisenseApiClient.Utils.HttpClient;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SisenseApiClient.Services.Jaql
{
    public class JaqlRunnerService : ServiceBase
    {
        public JaqlRunnerService(string serverUrl, IHttpClient httpClient, IAuthenticator authenticator)
            : base(serverUrl, httpClient, authenticator)
        {
            BasePath = "jaql/";
        }

        /// <summary>
        /// Runs a JAQL Query and returns the result as a string.
        /// </summary>
        /// <param name="jaqlQuery">JAQL Query to run.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<string> RunAsync(string jaqlQuery, CancellationToken cancellationToken)
        {
            return await SendRequestAsync(HttpMethod.Post, $"query", cancellationToken, jaqlQuery)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Runs a JAQL Query  and returns the result as a string.
        /// </summary>
        /// <param name="jaqlQuery">JAQL Query to run.</param>
        public async Task<string> RunAsync(string jaqlQuery)
        {
            return await RunAsync(jaqlQuery, CancellationToken.None)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Runs a JAQL Query and returns the result as an object of the specified type.
        /// </summary>
        /// <param name="jaqlQuery">JAQL Query to run.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<T> RunAsync<T>(string jaqlQuery, CancellationToken cancellationToken)
            where T : class
        {
            return await SendRequestAsync<T>(HttpMethod.Post, $"query", cancellationToken, jaqlQuery)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Runs a JAQL Query and returns the result as an object of the specified type.
        /// </summary>
        /// <param name="jaqlQuery">JAQL Query to run.</param>
        public async Task<T> RunAsync<T>(string jaqlQuery)
            where T : class
        {
            return await RunAsync<T>(jaqlQuery, CancellationToken.None)
                .ConfigureAwait(false);
        }
    }
}
