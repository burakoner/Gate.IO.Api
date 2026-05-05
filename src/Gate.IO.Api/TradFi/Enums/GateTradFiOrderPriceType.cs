namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order price type
/// </summary>
public enum GateTradFiOrderPriceType
{
    [Map("market")]
    Market = 1,

    [Map("trigger")]
    Trigger = 2,
}
