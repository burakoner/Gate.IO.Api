namespace Gate.IO.Api.Enums;

public enum OptionsTimeInForce
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
    /// PendingOrCancelled, makes a post-only order that always enjoys a maker fee
    /// </summary>
    [Label("poc")]
    PendingOrCancelled,
}