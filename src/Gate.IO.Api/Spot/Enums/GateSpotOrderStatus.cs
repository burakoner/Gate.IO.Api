namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Order Status
/// </summary>
public enum GateSpotOrderStatus
{
    /// <summary>
    /// Enum Open for value: open
    /// </summary>
    [Map("open")]
    Open,

    /// <summary>
    /// Enum Closed for value: closed
    /// </summary>
    [Map("closed")]
    Closed,

    /// <summary>
    /// Enum Cancelled for value: cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled
}