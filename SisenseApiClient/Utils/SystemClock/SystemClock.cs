
using System;

namespace SisenseApiClient.Utils.SystemClock
{
    public class SystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
