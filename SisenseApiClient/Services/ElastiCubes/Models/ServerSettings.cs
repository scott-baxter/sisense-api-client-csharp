namespace SisenseApiClient.Services.ElastiCubes.Models
{
    public class ServerSettings
    {
        public long DataImportChunkSize { get; set; }

        public string DefaultServerDataFolder { get; set; }

        public long ElasticubeMemoryAllocation { get; set; }

        public int ProcessorCount { get; set; }

        public long QueryTimeout { get; set; }

        public bool RecycleQueries { get; set; }

        public string RServer { get; set; }

        public bool RServerEnabled { get; set; }

        public int SimultaneousQueryExecutions { get; set; }
    }
}
