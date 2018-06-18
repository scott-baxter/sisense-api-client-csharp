using Newtonsoft.Json;
using SisenseApiClient.Services.ElastiCubes.Types;
using System;

namespace SisenseApiClient.Services.ElastiCubes.Models
{
    public class ServerStatus
    {
        public int Status { get; set; }

        public string Title { get; set; }

        [JsonIgnore]
        public ElastiCubeStatus StatusType
        {
            get
            {
                if (Enum.IsDefined(typeof(ElastiCubeStatus), Status))
                {
                    return (ElastiCubeStatus)Status;
                }

                return ElastiCubeStatus.Unknown;
            }
        }
    }
}
