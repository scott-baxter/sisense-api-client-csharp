using SisenseApiClient.Services.ElastiCubes.Types;
using System;

namespace SisenseApiClient.Services.ElastiCubes.Models
{
    public class ServerElastiCube
    {
        public class Cube
        {
            public DateTimeOffset CreatedUtc { get; set; }

            public string DatabaseName { get; set; }

            public DateTimeOffset LastBuiltUtc { get; set; }

            public int PermissionsSummary { get; set; }

            public double SizeInMb { get; set; }

            public ElastiCubeStatus Status { get; set; }
           
            public string Title { get; set; }
        }

        public string Address { get; set; }

        public string ErrorMessage { get; set; }

        public Cube[] Cubes { get; set; } = new Cube[] { };

        public int PermissionsSummary { get; set; }
    }
}
