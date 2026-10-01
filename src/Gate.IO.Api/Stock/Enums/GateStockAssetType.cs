namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock API asset type
/// </summary>
public enum GateStockAssetType
{
    /// <summary>Stock</summary>
    [Map("STOCK")]
    Stock,
    /// <summary>Exchange-traded fund</summary>
    [Map("ETF")]
    ExchangeTradedFund,
}
