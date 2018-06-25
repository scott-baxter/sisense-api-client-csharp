namespace SisenseApiClient.Services.ElastiCubes.Models
{
    public class MetadataField
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string DimType { get; set; }

        public string Title { get; set; }

        public string Table { get; set; }

        public string Column { get; set; }

        public bool? Merged { get; set; }

        public bool? Indexed { get; set; }

        public bool? DimensionTable { get; set; }
    }
}
