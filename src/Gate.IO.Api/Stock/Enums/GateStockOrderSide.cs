namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock order side
/// </summary>
public enum GateStockOrderSide
{
    /// <summary>Sell</summary>
    [Map("1")]
    Sell = 1,
    /// <summary>Buy</summary>
    [Map("2")]
    Buy = 2,
}
