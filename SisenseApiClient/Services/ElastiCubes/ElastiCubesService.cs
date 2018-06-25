using Newtonsoft.Json.Linq;
using SisenseApiClient.Authenticators;
using SisenseApiClient.Services.ElastiCubes.Models;
using SisenseApiClient.Services.ElastiCubes.Types;
using SisenseApiClient.Utils;
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

        #region v1.0

        /// <summary>
        /// Get a list of ElastiCube Sets. (v1.0)
        /// </summary>
        public async Task<IEnumerable<ElastiCubeSet>> GetSetsAsync()
        {
            return await SendRequestAsync<IEnumerable<ElastiCubeSet>>(HttpMethod.Get, "v1/elasticubes/sets")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Get an ElastiCube Set. (v1.0)
        /// </summary>
        /// <param name="setName">ElastiCube set name.</param>
        public async Task<ElastiCubeSet> GetSetAsync(string setName)
        {
            return await SendRequestAsync<ElastiCubeSet>(HttpMethod.Get, $"v1/elasticubes/sets/{setName}")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Get the ElastiCube build revision. (v1.0)
        /// </summary>
        /// <param name="server">The server of the ElastiCube.</param>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        public async Task<string> GetBuildRevisionAsync(string server, string cubeName)
        {
            return await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{cubeName}/revision")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Get an ElastiCube's custom tables. (v1.0)
        /// </summary>
        /// <param name="server">The server of the ElastiCube.</param>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        public async Task<IEnumerable<CustomTable>> GetCustomTablesAsync(string server, string cubeName)
        {
            string json = await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{cubeName}/custom_tables")
                .ConfigureAwait(false);

            JObject jObject = JObject.Parse(json);
            return jObject["tables"]?.ToObject<IEnumerable<CustomTable>>();
        }

        /// <summary>
        /// Get an ElastiCube table's relation. (v1.0)
        /// </summary>
        /// <param name="server">The server of the ElastiCube.</param>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        /// <param name="tableName">The name of the ElastiCube table.</param>
        public async Task<IEnumerable<TableRelation>> GetCustomTableRelationsAsync(string server, string cubeName, string tableName)
        {
            string json = await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{cubeName}/{tableName}/relations")
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

        /// <summary>
        /// Get an ElastiCube's custom table. (v1.0)
        /// </summary>
        /// <param name="server">The server of the ElastiCube.</param>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        /// <param name="tableName">The name of the ElastiCube table.</param>
        /// <returns></returns>
        public async Task<CustomTable> GetCustomTableAsync(string server, string cubeName, string tableName)
        {
            string json = await SendRequestAsync(HttpMethod.Get, $"v1/elasticubes/{server}/{cubeName}/custom_tables/{tableName}")
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

        /// <summary>
        /// Verify connectivity between your ElastiCube server and an R server. (v1.0)
        /// </summary>
        /// <param name="server">The ElastiCube server’s address.</param>
        /// <param name="rserver">The R server address.</param>
        public async Task<RServerConnectivity> VerifyConnectivityWithRServerAsync(string server, string rserver)
        {
            return await SendRequestAsync<RServerConnectivity>(HttpMethod.Get, $"v1/elasticubes/servers/{server}/settings/rserver/test?rserver={rserver}")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns your ElastiCube server’s settings. (v1.0)
        /// </summary>
        /// <param name="server">The ElastiCube server’s address.</param>
        public async Task<ServerSettings> GetServerSettingsAsync(string server)
        {
            return await SendRequestAsync<ServerSettings>(HttpMethod.Get, $"v1/elasticubes/servers/{server}/settings")
                .ConfigureAwait(false);
        }

        #endregion

        #region v0.9

        /// <summary>
        /// Returns a list of ElastiCubes with metadata. (v0.9)
        /// </summary>
        /// <param name="query">A query that returns all ElastiCubes beginning with the value. For example, a value of 'sa' will return ElastiCubes called 'Sample Ecommerce', 'Sample Lead generation' etc.</param>
        /// <param name="sortBy">The order in which the ElastiCubes appear in the response.</param>
        public async Task<IEnumerable<ServerElastiCubeMetadata>> GetElastiCubesMetadataAsync(string query = "", MetadataSortBy? sortBy = null)
        {
            string queryString = new QueryStringBuilder()
                .AddParameter("q", query)
                .AddParameter("sortby", sortBy?.ToString().ToLower())
                .Build();

            return await SendRequestAsync<IEnumerable<ServerElastiCubeMetadata>>(HttpMethod.Get, $"elasticubes/metadata{queryString}")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns metadata for an ElastiCube by ElastiCube name. (v0.9)
        /// </summary>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        public async Task<ServerElastiCubeMetadata> GetElastiCubeMetadataAsync(string cubeName)
        {
            return await SendRequestAsync<ServerElastiCubeMetadata>(HttpMethod.Get, $"elasticubes/metadata/{cubeName}")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns fields included in a specific ElastiCube. (v0.9)
        /// </summary>
        /// <param name="cubeName">The name of the ElastiCube.</param>
        /// <param name="query">Enter a specific query to return only fields that include the query string.</param>
        /// <param name="offset">Defines how many items to skip before returning the results. For example, to return results from value #101 onward, enter a value of '100'.</param>
        /// <param name="count">Limits the result set to a defined Integer of results. Enter 0 (zero) or leave blank not to limit.</param>
        public async Task<IEnumerable<MetadataField>> GetElastiCubeMetadataFieldsAsync(string cubeName, string query = "", 
            int? offset = null, int? count = null)
        {
            string queryString = new QueryStringBuilder()
                .AddParameter("q", query)
                .AddParameter("offset", offset?.ToString())
                .AddParameter("count", count?.ToString())
                .Build();

            return await SendRequestAsync<IEnumerable<MetadataField>>(HttpMethod.Get, $"elasticubes/metadata/{cubeName}/fields{queryString}")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns ElastiCubes with their server and ElastiCube details. (v0.9)
        /// </summary>
        /// <param name="query">A query that returns all ElastiCubes beginning with the value. For example, a value of 'sa' will return ElastiCubes called 'Sample Ecommerce', 'Sample Lead generation', etc.</param>
        /// <param name="offset">>Defines how many items to skip before returning the results. For example, to return results from value #101 onward, enter a value of '100'.</param>
        /// <param name="count">Limits the result set to a defined Integer of results. Enter 0 (zero) or leave blank not to limit.</param>
        /// <param name="direction">The sort direction of the results.</param>
        /// <param name="withPermissions">Include ElastiCube permissions.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerElastiCube>> GetServersElastiCubesAsync(string query = "", int? offset = null, int? count = null, 
            string direction = "", bool? withPermissions = null)
        {
            string queryString = new QueryStringBuilder()
                .AddParameter("q", query)
                .AddParameter("offset", offset?.ToString())
                .AddParameter("count", count?.ToString())
                .AddParameter("direction", direction)
                .AddParameter("withPermissions", withPermissions?.ToString())
                .Build();

            // IMPORTANT: the uri MUST be "elasticubes/", with the "/" otherwise you will have a 403
            return await SendRequestAsync<IEnumerable<ServerElastiCube>>(HttpMethod.Get, $"elasticubes/{queryString}")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the ElastiCube servers with metadata. (v0.9)
        /// </summary>
        /// <param name="query">A query that returns all ElastiCubes beginning with the value. For example, a value of 'sa' will return ElastiCubes called 'Sample Ecommerce', 'Sample Lead generation', etc.</param>
        /// <param name="offset">>Defines how many items to skip before returning the results. For example, to return results from value #101 onward, enter a value of '100'.</param>
        /// <param name="count">Limits the result set to a defined Integer of results. Enter 0 (zero) or leave blank not to limit.</param>
        /// <param name="direction">The sort direction of the results.</param>
        /// <param name="withPermissions">Include ElastiCube permissions.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ServerMetadata>> GetServersMetadataAsync(string query = "", int? offset = null, int? count = null,
            string direction = "", bool? withPermissions = null)
        {
            string queryString = new QueryStringBuilder()
                .AddParameter("q", query)
                .AddParameter("offset", offset?.ToString())
                .AddParameter("count", count?.ToString())
                .AddParameter("direction", direction)
                .AddParameter("withPermissions", withPermissions?.ToString())
                .Build();

            return await SendRequestAsync<IEnumerable<ServerMetadata>>(HttpMethod.Get, $"elasticubes/servers{queryString}")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns all the ElastiCubes by server. (v0.9)
        /// </summary>
        /// <param name="server">The server address. The default value that you can enter is localhost.</param>
        /// <param name="query">A query that returns all ElastiCubes beginning with the value. For example, a value of 'sa' will return ElastiCubes called 'Sample Ecommerce', 'Sample Lead generation', etc.</param>
        /// <param name="offset">>Defines how many items to skip before returning the results. For example, to return results from value #101 onward, enter a value of '100'.</param>
        /// <param name="count">Limits the result set to a defined Integer of results. Enter 0 (zero) or leave blank not to limit.</param>
        /// <param name="orderBy">Orders the results by field name. You can add multiple sort fields separated by a comma delimiter ','</param>
        /// <param name="direction">The sort direction of the results.</param>
        public async Task<IEnumerable<ElastiCube>> GetElastiCubesByServerAsync(string server, string query = "", int? offset = null, int? count = null,
            string orderBy = "", string direction = "")
        {
            string queryString = new QueryStringBuilder()
                .AddParameter("q", query)
                .AddParameter("offset", offset?.ToString())
                .AddParameter("count", count?.ToString())
                .AddParameter("orderBy", orderBy)
                .AddParameter("direction", direction)
                .Build();

            return await SendRequestAsync<IEnumerable<ElastiCube>>(HttpMethod.Get, $"elasticubes/servers/{server}{queryString}")
                .ConfigureAwait(false);
        }

        // TODO: endpoint not working, report
        //public async Task<string> GetTableCustomFieldsAsync(string server, string database = "")
        //{
        //    return await SendRequestAsync(HttpMethod.Get, $"elasticubes/servers/{server}/simple?database={database}")
        //        .ConfigureAwait(false);
        //}

        /// <summary>
        /// Returns the status of each ElastiCube in the selected server. (v0.9)
        /// </summary>
        /// <param name="server">The server address. The default value that you can enter is localhost.</param>
        /// <param name="query">A query that returns all ElastiCubes beginning with the value. For example, a value of 'sa' will return ElastiCubes called 'Sample Ecommerce', 'Sample Lead generation', etc.</param>
        /// <param name="offset">>Defines how many items to skip before returning the results. For example, to return results from value #101 onward, enter a value of '100'.</param>
        /// <param name="count">Limits the result set to a defined Integer of results. Enter 0 (zero) or leave blank not to limit.</param>
        /// <param name="orderBy">Orders the results by field name. You can add multiple sort fields separated by a comma delimiter ','</param>
        /// <param name="direction">The sort direction of the results.</param>
        public async Task<IEnumerable<ServerStatus>> GetServerStatusAsync(string server, string query = "", int? offset = null, int? count = null,
            string orderBy = "", string direction = "")
        {
            return await SendRequestAsync<IEnumerable<ServerStatus>>(HttpMethod.Get, $"elasticubes/servers/{server}/status")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns all authentication records for the given ElastiCube. (v0.9)
        /// </summary>
        /// <param name="server">The ElastiCube's server address.</param>
        /// <param name="cubeName">The name or ID of the ElastiCube.</param>
        public async Task<ElastiCubePermissions> GetElastiCubePermissionsAsync(string server, string cubeName)
        {
            return await SendRequestAsync<ElastiCubePermissions>(HttpMethod.Get, $"elasticubes/{server}/{cubeName}/permissions")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Starts the ElastiCube Server.
        /// </summary>
        /// <param name="server">The ElastiCube's server address.</param>
        /// <param name="cubeName">The name or ID of the ElastiCube.</param>
        public async Task StartElastiCubeServerAsync(string server, string cubeName)
        {
            await SendRequestAsync(HttpMethod.Post, $"elasticubes/{server}/{cubeName}/start")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Stops the ElastiCube Server.
        /// </summary>
        /// <param name="server">The ElastiCube's server address.</param>
        /// <param name="cubeName">The name or ID of the ElastiCube.</param>
        public async Task StopElastiCubeServerAsync(string server, string cubeName)
        {
            await SendRequestAsync(HttpMethod.Post, $"elasticubes/{server}/{cubeName}/stop")
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Restarts the ElastiCube Server.
        /// </summary>
        /// <param name="server">The ElastiCube's server address.</param>
        /// <param name="cubeName">The name or ID of the ElastiCube.</param>
        public async Task RestartElastiCubeServerAsync(string server, string cubeName)
        {
            await SendRequestAsync(HttpMethod.Post, $"elasticubes/{server}/{cubeName}/restart")
                .ConfigureAwait(false);
        }

        #endregion
    }
}
