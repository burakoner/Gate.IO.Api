namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Account Type
/// </summary>
public enum GateSpotAccountType : byte
{
    /// <summary>
    /// Spot
    /// </summary>
    [Map("spot")]
    Spot = 1,

    /// <summary>
    /// Margin
    /// </summary>
    [Map("margin")]
    Margin = 2,

    /// <summary>
    /// Unified
    /// </summary>
    [Map("unified")]
    Unified = 3,

    /// <summary>
    /// CrossMargin
    /// </summary>
    [Map("cross_margin")]
    CrossMargin = 4,
}