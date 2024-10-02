namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Account Type
/// </summary>
public enum GateSpotAccountType
{
    /// <summary>
    /// Spot
    /// </summary>
    [Map("spot")]
    Spot,

    /// <summary>
    /// Margin
    /// </summary>
    [Map("margin")]
    Margin,

    /// <summary>
    /// Unified
    /// </summary>
    [Map("unified")]
    Unified,

    /// <summary>
    /// CrossMargin
    /// </summary>
    [Map("cross_margin")]
    CrossMargin,
}