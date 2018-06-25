using SisenseApiClient.Services.ElastiCubes.Types;

namespace SisenseApiClient.Services.ElastiCubes.Models
{
    public class ServerStatus
    {
        public ElastiCubeStatus Status { get; set; }

        public string Title { get; set; }
    }
}
