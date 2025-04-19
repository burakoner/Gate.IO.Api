namespace Gate.IO.Api.Spot;

/// <summary>
/// Trading pair type, normal: normal, premarket: pre-market
/// </summary>
public enum GateSpotMarketType
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("normal")]
    Normal = 1,

    /// <summary>
    /// Premarket
    /// </summary>
    [Map("premarket")]
    Premarket = 2,
}
