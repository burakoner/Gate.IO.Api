namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock order price type
/// </summary>
public enum GateStockOrderPriceType
{
    /// <summary>Market order</summary>
    [Map("market")]
    Market,
    /// <summary>Limit order</summary>
    [Map("limit")]
    Limit,
}
