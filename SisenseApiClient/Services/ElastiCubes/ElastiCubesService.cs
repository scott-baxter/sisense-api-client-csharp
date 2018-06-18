using Newtonsoft.Json.Linq;
using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.ElastiCubes.Models;
using SisenseApiClient.Utils.HttpClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SisenseApiClient.Services.ElastiCubes
{
    public class ElastiCubesService : ServiceBase
    {
        public ElastiCubesService(string serverUrl, IHttpClient httpClient, IAuthenticator authenticator) 
            : base(serverUrl, httpClient, authenticator)
        {
        }

        #region v1

        public async Task<IEnumerable<ElastiCubeSet>> GetSetsAsync()
        {
            return await SendRequestAsync<IEnumerable<ElastiCubeSet>>(HttpMethod.Get, "v1/elasticubes/sets")
                .ConfigureAwait(false);
        }

        public async Task<ElastiCubeSet> GetSetAsync(string setName)
        {
            return await SendRequestAsync<ElastiCubeSet>(HttpMethod.Get, $"v1/elasticubes/sets/{setName}")
                .ConfigureAwait(false);
        }

        public async Task<string> GetBuildRevisionAsync(string server, string cubeName)
        {
            return await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{cubeName}/revision")
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomTable>> GetCustomTablesAsync(string server, string cubeName)
        {
            string json = await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{cubeName}/custom_tables")
                .ConfigureAwait(false);

            JObject jObject = JObject.Parse(json);
            return jObject["tables"]?.ToObject<IEnumerable<CustomTable>>();
        }

        public async Task<IEnumerable<TableRelation>> GetCustomTableRelationsAsync(string server, string cubeName, string table)
        {
            string json = await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{cubeName}/{table}/relations")
                .ConfigureAwait(false);

            JObject jObject = JObject.Parse(json);
            return jObject["relations"]?.ToObject<IEnumerable<TableRelation>>();
        }

        // TODO: endpoint not working, report
        //public async Task<string> GetTableCustomFieldsAsync(string server, string title, string table)
        //{
        //    return await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{title}/{table}/custom_fields")
        //        .ConfigureAwait(false);
        //}

        public async Task<CustomTable> GetCustomTableAsync(string server, string cubeName, string table)
        {
            string json = await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{cubeName}/custom_tables/{table}")
                .ConfigureAwait(false);

            JObject jObject = JObject.Parse(json);
            return jObject["table"]?.ToObject<CustomTable>();
        }

        // TODO: check how to retrieve a query from a table
        // Custom Error 500?
        //public async Task<string> GetTableSQLAsync(string server, string title, string table)
        //{
        //    return await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{title}/sql_manual_query/{table}")
        //        .ConfigureAwait(false);
        //}

        // TODO: endpoint custom fields not working, check if possible to test
        //public async Task<string> GetTableCustomFieldsAsync(string server, string title, string table, string fieldName)
        //{
        //    return await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{title}/{table}/custom_fields/{fieldName}")
        //        .ConfigureAwait(false);
        //}

        // TODO: endpoint only works if rserver is correct, otherwise 500 errors
        public async Task<RServerConnectivity> VerifyConnectivityWithRServerAsync(string server, string rserver)
        {
            return await SendRequestAsync<RServerConnectivity>(HttpMethod.Get, $"v1/elasticubes/servers/{server}/settings/rserver/test?rserver={rserver}")
                .ConfigureAwait(false);
        }

        public async Task<ServerSettings> GetServerSettingsAsync(string server)
        {
            return await SendRequestAsync<ServerSettings>(HttpMethod.Get, $"v1/elasticubes/servers/{server}/settings")
                .ConfigureAwait(false);
        }



        // TODO: Not yet
        //public async Task UpdateElastiCubeSetAsync(string title, BasicElastiCubeSet basicElastiCubeSet)
        //{
        //    await SendRequestAsync(HttpMethodUtils.Patch, $"v1/elasticubes/sets/{title}", basicElastiCubeSet)
        //        .ConfigureAwait(false);
        //}

        #endregion

        #region v09

        //public async Task<IEnumerable<ServerStatus>> GetServerStatusAsync(string server)
        //{
        //    return await SendRequestAsync<IEnumerable<ServerStatus>>(HttpMethod.Get, $"elasticubes/servers/{server}/status")
        //        .ConfigureAwait(false);
        //}

        //public async Task StartBuildElastiCubeAsync(string server, string elastiCube, BuildType? buildType = null)
        //{
        //    var queryString = new QueryStringBuilder()
        //        .AddParameter("type", buildType?.ToString())
        //        .Build();

        //    await SendRequestAsync(HttpMethod.Post, $"elasticubes/{server}/{elastiCube}/startBuild{queryString}")
        //        .ConfigureAwait(false);
        //}

        #endregion
    }
}
