namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsTimeInForce
/// </summary>
public enum GateOptionsTimeInForce : byte
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
    /// PendingOrCancelled, makes a post-only order that always enjoys a maker fee
    /// </summary>
    [Map("poc")]
    PendingOrCancelled = 3,
}