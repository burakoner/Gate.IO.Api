namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order price type
/// </summary>
public enum GateTradFiOrderPriceType
{
    /// <summary>
    /// Represents the Market value.
    /// </summary>
    [Map("market")]
    Market = 1,

    /// <summary>
    /// Represents the Trigger value.
    /// </summary>
    [Map("trigger")]
    Trigger = 2,
}
