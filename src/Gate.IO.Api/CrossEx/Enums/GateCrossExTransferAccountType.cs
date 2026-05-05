namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account type for transfers
/// </summary>
public enum GateCrossExTransferAccountType
{
    [Map("CROSSEX_BINANCE")]
    CrossExBinance = 1,

    [Map("CROSSEX_OKX")]
    CrossExOkx = 2,

    [Map("CROSSEX_GATE")]
    CrossExGate = 3,

    [Map("CROSSEX_BYBIT")]
    CrossExBybit = 4,

    [Map("CROSSEX")]
    CrossEx = 5,

    [Map("SPOT")]
    Spot = 6,
}
