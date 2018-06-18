using Newtonsoft.Json;
using System.Collections.Generic;

namespace SisenseApiClient.Services.ElastiCubes.Models
{
    public class ElastiCubeSet
    {
        public class Share
        {
            public string PartyId { get; set; }

            public string Type { get; set; }

            public string Permission { get; set; }
        }

        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }

        public IEnumerable<string> FullNames { get; set; }

        public string RoutingMode { get; set; }

        public string Title { get; set; }

        public string Creator { get; set; }

        public IEnumerable<Share> Shares { get; set; }

        public string Failover { get; set; }
    }
}
