namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi trading status
/// </summary>
public enum GateTradFiTradingStatus
{
    [Map("open")]
    Open = 1,

    [Map("closed")]
    Closed = 2,
}
