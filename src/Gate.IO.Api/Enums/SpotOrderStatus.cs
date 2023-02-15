namespace Gate.IO.Api.Enums;

/// <summary>
/// Spot Order Status
/// </summary>
public enum SpotOrderStatus
{
    /// <summary>
    /// Enum Open for value: open
    /// </summary>
    [EnumMember(Value = "open")]
    Open = 1,

    /// <summary>
    /// Enum Closed for value: closed
    /// </summary>
    [EnumMember(Value = "closed")]
    Closed = 2,

    /// <summary>
    /// Enum Cancelled for value: cancelled
    /// </summary>
    [EnumMember(Value = "cancelled")]
    Cancelled = 3
}