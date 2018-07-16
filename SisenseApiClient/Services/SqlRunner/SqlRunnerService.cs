using SisenseApiClient.Authenticators;
using SisenseApiClient.Utils.HttpClient;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SisenseApiClient.Services.SqlRunner
{
    public class SqlRunnerService : ServiceBase
    {
        public SqlRunnerService(string serverUrl, IHttpClient httpClient, IAuthenticator authenticator)
            : base(serverUrl, httpClient, authenticator)
        {
        }

        /// <summary>
        /// Runs a SQL Query and returns the result as a JSON string.
        /// </summary>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        /// <param name="sqlQuery">SQL Query to run.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<string> RunAsync(string cubeName, string sqlQuery, CancellationToken cancellationToken)
        {
            return await SendRequestAsync(HttpMethod.Get, $"datasources/{cubeName}/sql?query={sqlQuery}", cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Runs a SQL Query and returns the result as a JSON string.
        /// </summary>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        /// <param name="sqlQuery">SQL Query to run.</param>
        public async Task<string> RunAsync(string cubeName, string sqlQuery)
        {
            return await RunAsync(cubeName, sqlQuery, CancellationToken.None)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Runs a SQL Query and returns the result as an object of the specified type.
        /// </summary>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        /// <param name="sqlQuery">SQL Query to run.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<T> RunAsync<T>(string cubeName, string sqlQuery, CancellationToken cancellationToken)
            where T : class
        {
            return await SendRequestAsync<T>(HttpMethod.Get, $"datasources/{cubeName}/sql?query={sqlQuery}", cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Runs a SQL Query and returns the result as an object of the specified type.
        /// </summary>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        /// <param name="sqlQuery">SQL Query to run.</param>
        public async Task<T> RunAsync<T>(string cubeName, string sqlQuery)
            where T : class
        {
            return await RunAsync<T>(cubeName, sqlQuery, CancellationToken.None)
                .ConfigureAwait(false);
        }
    }
}
