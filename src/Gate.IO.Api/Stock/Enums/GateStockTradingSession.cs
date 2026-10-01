namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock trading session
/// </summary>
public enum GateStockTradingSession
{
    /// <summary>Regular trading hours; supported by market orders only</summary>
    [Map("regular")]
    Regular,
    /// <summary>All supported sessions; required for limit orders</summary>
    [Map("all")]
    All,
}
