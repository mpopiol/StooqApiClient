using System;

namespace StooqApiClient.Enums
{
    [Flags]
    public enum Skip
    {
        None = 0,
        Others = 1,
        Denominations = 2,
        PreaccessionRights = 4,
        PrepurchaseRights = 8,
        PreemptiveRights = 16,
        Dividends = 32,
        Splits = 64
    }
}
