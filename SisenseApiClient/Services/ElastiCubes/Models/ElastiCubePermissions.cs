using Newtonsoft.Json;
using System.Collections.Generic;

namespace SisenseApiClient.Services.ElastiCubes.Models
{
    public class ElastiCubePermissions
    {
        public class Share
        {
            public string PartyId { get; set; }

            public string Type { get; set; }

            public string Permission { get; set; }
        }

        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }

        public string Server { get; set; }

        public string Title { get; set; }

        public IEnumerable<Share> Shares { get; set; }

        public string Creator { get; set; }
    }
}
