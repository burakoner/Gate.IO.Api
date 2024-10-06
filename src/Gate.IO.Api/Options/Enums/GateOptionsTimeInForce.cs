namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsTimeInForce
/// </summary>
public enum GateOptionsTimeInForce
{
    /// <summary>
    /// GoodTillCancelled
    /// </summary>
    [Map("gtc")]
    GoodTillCancelled,

    /// <summary>
    /// ImmediateOrCancelled, taker only
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancel,

    /// <summary>
    /// PendingOrCancelled, makes a post-only order that always enjoys a maker fee
    /// </summary>
    [Map("poc")]
    PendingOrCancelled,
}