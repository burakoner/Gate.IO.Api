namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi trading status
/// </summary>
public enum GateTradFiTradingStatus
{
    /// <summary>
    /// Represents the Open value.
    /// </summary>
    [Map("open")]
    Open = 1,

    /// <summary>
    /// Represents the Closed value.
    /// </summary>
    [Map("closed")]
    Closed = 2,
}
