namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotPriceTriggeredOrderStatus
/// </summary>
public enum GateSpotTriggerStatus
{
    /// <summary>
    /// Open
    /// </summary>
    [Map("open")]
    Open,

    /// <summary>
    /// Cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled,

    /// <summary>
    /// Finished
    /// </summary>
    [Map("finish")]
    Finished,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed,

    /// <summary>
    /// Expired
    /// </summary>
    [Map("expired")]
    Expired,
}
