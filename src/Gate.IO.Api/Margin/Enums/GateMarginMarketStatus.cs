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
    Disabled = 0,

    /// <summary>
    /// Enabled
    /// </summary>
    [Map("1")]
    Enabled = 1,
}
