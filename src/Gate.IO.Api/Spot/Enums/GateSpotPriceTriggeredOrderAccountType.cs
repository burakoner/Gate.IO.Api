namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot price-triggered order account type
/// </summary>
public enum GateSpotPriceTriggeredOrderAccountType : byte
{
    /// <summary>
    /// Spot trading
    /// </summary>
    [Map("normal")]
    Normal = 1,

    /// <summary>
    /// Margin trading
    /// </summary>
    [Map("margin")]
    Margin = 2,

    /// <summary>
    /// Unified account
    /// </summary>
    [Map("unified")]
    Unified = 3,
}
