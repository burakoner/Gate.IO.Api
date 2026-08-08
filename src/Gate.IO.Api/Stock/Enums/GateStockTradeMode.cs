namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock trading permission mode
/// </summary>
public enum GateStockTradeMode
{
    /// <summary>Trading disabled</summary>
    [Map("0")]
    Disabled = 0,
    /// <summary>Buy only</summary>
    [Map("1")]
    BuyOnly = 1,
    /// <summary>Sell only</summary>
    [Map("2")]
    SellOnly = 2,
    /// <summary>Buy and sell</summary>
    [Map("4")]
    BuyAndSell = 4,
}
