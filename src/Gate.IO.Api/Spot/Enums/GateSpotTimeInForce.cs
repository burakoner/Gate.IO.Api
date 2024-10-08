namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Order Time-In-Force
/// </summary>
public enum GateSpotTimeInForce
{
    /// <summary>
    /// GoodTillCancelled
    /// </summary>
    [Map("gtc")]
    GoodTillCancelled = 1,

    /// <summary>
    /// ImmediateOrCancelled, taker only
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancel = 2,

    /// <summary>
    /// FillOrKill, fill either completely or none Only. ioc and fok are supported when type=market
    /// </summary>
    [Map("fok")]
    FillOrKill = 3,

    /// <summary>
    /// PendingOrCancelled, makes a post-only order that always enjoys a maker fee
    /// </summary>
    [Map("poc")]
    PendingOrCancelled = 4,
}