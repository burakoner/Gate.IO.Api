namespace Gate.IO.Api.Enums;

/// <summary>
/// Spot Order Type
/// </summary>
public enum SpotOrderType
{
    /// <summary>
    /// Limit Order
    /// </summary>
    [Label("limit")]
    Limit,

    /// <summary>
    /// Market Order
    /// </summary>
    [Label("market")]
    Market
}