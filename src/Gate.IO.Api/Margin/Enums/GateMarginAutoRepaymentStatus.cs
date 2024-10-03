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
    Enabled,

    /// <summary>
    /// Disabled
    /// </summary>
    [Map("off")]
    Disabled,
}