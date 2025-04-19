namespace Gate.IO.Api.Margin;

/// <summary>
/// GateMarginAutoRepaymentStatus
/// </summary>
public enum GateMarginAutoRepaymentStatus : byte
{
    /// <summary>
    /// Disabled
    /// </summary>
    [Map("off")]
    Disabled = 0,

    /// <summary>
    /// Enabled
    /// </summary>
    [Map("on")]
    Enabled = 1,
}