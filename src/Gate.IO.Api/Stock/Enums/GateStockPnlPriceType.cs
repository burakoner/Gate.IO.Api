namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock profit and loss price source
/// </summary>
public enum GateStockPnlPriceType
{
    /// <summary>Intraday price</summary>
    [Map("1")]
    Intraday = 1,
    /// <summary>Latest extended-hours price</summary>
    [Map("2")]
    ExtendedHours = 2,
}
