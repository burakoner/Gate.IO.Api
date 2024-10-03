namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Market Status
/// </summary>
public enum GateMarginMarketStatus
{
    /// <summary>
    /// Disabled
    /// </summary>
    [Map("0")]
    Disabled,

    /// <summary>
    /// Enabled
    /// </summary>
    [Map("1")]
    Enabled,
}
