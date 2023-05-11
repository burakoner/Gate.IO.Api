namespace Gate.IO.Api.Enums;

/// <summary>
/// Spot Order Status
/// </summary>
public enum SpotOrderStatus
{
    /// <summary>
    /// Enum Open for value: open
    /// </summary>
    [Label("open")]
    Open,

    /// <summary>
    /// Enum Closed for value: closed
    /// </summary>
    [Label("closed")]
    Closed,

    /// <summary>
    /// Enum Cancelled for value: cancelled
    /// </summary>
    [Label("cancelled")]
    Cancelled
}