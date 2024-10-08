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
    GoodTillCancelled = 1,

    /// <summary>
    /// ImmediateOrCancelled, taker only
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancel = 2,
}