namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Order Type
/// </summary>
public enum GateSpotOrderType
{
    /// <summary>
    /// Limit Order
    /// </summary>
    [Map("limit")]
    Limit,

    /// <summary>
    /// Market Order
    /// </summary>
    [Map("market")]
    Market
}