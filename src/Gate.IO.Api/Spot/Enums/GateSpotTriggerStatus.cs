namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotPriceTriggeredOrderStatus
/// </summary>
public enum GateSpotTriggerStatus : byte
{
    /// <summary>
    /// Open
    /// </summary>
    [Map("open")]
    Open = 1,

    /// <summary>
    /// Cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled = 2,

    /// <summary>
    /// Finished
    /// </summary>
    [Map("finish")]
    Finished = 3,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed = 4,

    /// <summary>
    /// Expired
    /// </summary>
    [Map("expired")]
    Expired = 5,
}
