namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx time in force
/// </summary>
public enum GateCrossExTimeInForce
{
    [Map("GTC")]
    GoodTillCancelled = 1,

    [Map("IOC")]
    ImmediateOrCancelled = 2,

    [Map("FOK")]
    FillOrKill = 3,

    [Map("POC")]
    PendingOrCancelled = 4,
}
