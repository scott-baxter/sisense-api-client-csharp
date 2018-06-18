namespace SisenseApiClient.Services.ElastiCubes.Types
{
    public enum ElastiCubeStatus
    {
        Unknown = 0,
        Stopped = 1,
        Running = 2,
        Faulted = 4,
        BeingDeleted = 8,
        CurrentlyRestarting = 16,
        WrongVersion = 32,
        IsDown32Bit = 64,
        IsDown64Bit = 128,
        Locked = 256,
        CubeOrChildBuilding = 512,
        Starting = 1024,
        Building = 2048,
        TryingImportBigData = 4096,
        TryingImportNonBigData = 8192,
        PostIndexing = 16384,
        StoppedButStillRunning = 32768,
        CancellingBuild = 65536
    }
}
