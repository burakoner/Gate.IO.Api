namespace Gate.IO.Api.Margin;

/// <summary>
/// GateMarginAutoRepaymentStatus
/// </summary>
public enum GateMarginAutoRepaymentStatus
{
    /// <summary>
    /// Enabled
    /// </summary>
    [Map("on")]
    Enabled = 1,

    /// <summary>
    /// Disabled
    /// </summary>
    [Map("off")]
    Disabled = 2,
}