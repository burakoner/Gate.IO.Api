namespace Gate.IO.Api.Enums;

public enum SpotTriggerOrderTimeInForce
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
}