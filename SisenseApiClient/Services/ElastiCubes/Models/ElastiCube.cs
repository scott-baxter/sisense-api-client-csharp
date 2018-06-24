using Newtonsoft.Json;
using SisenseApiClient.Services.ElastiCubes.Types;
using System;
using System.Collections.Generic;

namespace SisenseApiClient.Services.ElastiCubes.Models
{
    public class ElastiCube
    {
        public class Share
        {
            public bool BelongsToEveryoneGroup { get; set; }

            public string PartyId { get; set; }

            public string Type { get; set; }

            public string Permission { get; set; }
        }

        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public string DatabaseName { get; set; }

        public DateTimeOffset LastBuiltUtc { get; set; }

        public int PermissionsSummary { get; set; }

        public double SizeInMb { get; set; }

        public ElastiCubeStatus Status { get; set; }

        public string Title { get; set; }

        public string Error { get; set; }

        public IEnumerable<Share> Shares { get; set; }
    }
}
