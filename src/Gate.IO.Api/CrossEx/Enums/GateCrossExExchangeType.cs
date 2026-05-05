namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx exchange type
/// </summary>
public enum GateCrossExExchangeType
{
    [Map("BINANCE")]
    Binance = 1,

    [Map("OKX")]
    Okx = 2,

    [Map("GATE")]
    Gate = 3,

    [Map("BYBIT")]
    Bybit = 4,

    [Map("CROSSEX")]
    CrossEx = 5,
}
