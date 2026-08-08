namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx time in force
/// </summary>
public enum GateCrossExTimeInForce
{
    /// <summary>
    /// Represents the Good Till Cancelled value.
    /// </summary>
    [Map("GTC")]
    GoodTillCancelled = 1,

    /// <summary>
    /// Represents the Immediate Or Cancelled value.
    /// </summary>
    [Map("IOC")]
    ImmediateOrCancelled = 2,

    /// <summary>
    /// Represents the Fill Or Kill value.
    /// </summary>
    [Map("FOK")]
    FillOrKill = 3,

    /// <summary>
    /// Represents the Pending Or Cancelled value.
    /// </summary>
    [Map("POC")]
    PendingOrCancelled = 4,

    /// <summary>
    /// Represents the Retail Price Improvement value.
    /// </summary>
    [Map("RPI")]
    RetailPriceImprovement = 5,
}
