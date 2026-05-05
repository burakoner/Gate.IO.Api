namespace Gate.IO.Api.Options;

/// <summary>
/// GateOptionsMarginMode
/// </summary>
public enum GateOptionsMarginMode : byte
{
    /// <summary>
    /// Classic Spot Margin Mode
    /// </summary>
    [Map("0")]
    ClassicSpotMarginMode = 0,

    /// <summary>
    /// Cross Currency Margin Mode
    /// </summary>
    [Map("1")]
    CrossCurrencyMarginMode = 1,

    /// <summary>
    /// Portfolio Margin Mode
    /// </summary>
    [Map("2")]
    PortfolioMarginMode = 2,

    /// <summary>
    /// Combined Margin Mode
    /// </summary>
    [Map("2")]
    CombinedMarginMode = 2,

    /// <summary>
    /// Single Currency Margin Mode
    /// </summary>
    [Map("3")]
    SingleCurrencyMarginMode = 3,
}
