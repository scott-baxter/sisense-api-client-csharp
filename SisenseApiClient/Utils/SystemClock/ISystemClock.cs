using System;

namespace SisenseApiClient.Utils.SystemClock
{
    public interface ISystemClock
    {
        DateTimeOffset UtcNow { get; }
    }
}