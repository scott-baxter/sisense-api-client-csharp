namespace SisenseApiClient.Services.ElastiCubes.Types
{
    public enum BuildType
    {
        /// <summary>
        /// Updates the ElastiCube server with the ElastiCube schema, without building.
        /// </summary>
        None,
        /// <summary>
        /// Rebuilds the ElastiCube from scratch.
        /// </summary>
        Full,
        /// <summary>
        /// Rebuilds from scratch tables that have changed in the ElastiCube schema.
        /// </summary>
        Delta,
        /// <summary>
        /// Rebuilds the ElastiCube and accumulates data for tables marked as accumulative. This mode should only be used for accumulative builds.
        /// </summary>
        FullUpdateExisting
    }
}
