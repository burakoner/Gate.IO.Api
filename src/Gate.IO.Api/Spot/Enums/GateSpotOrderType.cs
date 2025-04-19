namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot Order Type
/// </summary>
public enum GateSpotOrderType : byte
{
    /// <summary>
    /// Limit Order
    /// </summary>
    [Map("limit")]
    Limit = 1,

    /// <summary>
    /// Market Order
    /// </summary>
    [Map("market")]
    Market = 2
}