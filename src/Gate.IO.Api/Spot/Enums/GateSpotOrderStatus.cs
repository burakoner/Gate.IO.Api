namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Order Status
/// </summary>
public enum GateSpotOrderStatus : byte
{
    /// <summary>
    /// Enum Open for value: open
    /// </summary>
    [Map("open")]
    Open = 1,

    /// <summary>
    /// Enum Closed for value: closed
    /// </summary>
    [Map("closed")]
    Closed = 2,

    /// <summary>
    /// Enum Cancelled for value: cancelled
    /// </summary>
    [Map("cancelled")]
    Cancelled = 3
}