namespace Gate.IO.Api.Enums;

/// <summary>
/// Futures Order Time-In-Force
/// </summary>
public enum FuturesTimeInForce
{
    /// <summary>
    /// GoodTillCancelled
    /// </summary>
    [Label("gtc")]
    GoodTillCancelled,

    /// <summary>
    /// ImmediateOrCancelled, taker only
    /// </summary>
    [Label("ioc")]
    ImmediateOrCancel,

    /// <summary>
    /// FillOrKill, fill either completely or none Only. ioc and fok are supported when type=market
    /// </summary>
    [Label("fok")]
    FillOrKill,

    /// <summary>
    /// PendingOrCancelled, makes a post-only order that always enjoys a maker fee
    /// </summary>
    [Label("poc")]
    PendingOrCancelled,
}