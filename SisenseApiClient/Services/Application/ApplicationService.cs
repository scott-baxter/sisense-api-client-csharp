using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.Application.Models;
using SisenseApiClient.Utils.HttpClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace SisenseApiClient.Services.Application
{
    public class ApplicationService : ServiceBase
    {
        public ApplicationService(string serverUrl, IHttpClient httpClient, IAuthenticator authenticator) 
            : base(serverUrl, httpClient, authenticator)
        {
        }

        /// <summary>
        /// Provides information on the current status of the Sisense application.
        /// </summary>
        public async Task<ApplicationStatus> GetStatusAsync()
        {
            return await SendRequestAsync<ApplicationStatus>(HttpMethod.Get, "v1/application/status")
                .ConfigureAwait(false);
        }
    }
}
