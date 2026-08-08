namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock trading session
/// </summary>
public enum GateStockTradingSession
{
    /// <summary>Regular trading hours</summary>
    [Map("regular")]
    Regular,
    /// <summary>All supported sessions</summary>
    [Map("all")]
    All,
}
