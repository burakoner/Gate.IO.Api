namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsMarginMode
/// </summary>
public enum GateOptionsMarginMode : byte
{
    /// <summary>
    /// Classic Spot Margin Mode
    /// </summary>
    [Map("long")]
    ClassicSpotMarginMode = 0,

    /// <summary>
    /// Cross Currency Margin Mode
    /// </summary>
    [Map("short")]
    CrossCurrencyMarginMode = 1,

    /// <summary>
    /// Combined Margin Mode
    /// </summary>
    [Map("short")]
    CombinedMarginMode = 2,
}