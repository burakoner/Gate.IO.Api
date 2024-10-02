namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotTriggerTimeInForce
/// </summary>
public enum GateSpotTriggerTimeInForce
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
}