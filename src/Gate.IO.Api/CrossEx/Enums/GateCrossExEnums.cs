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

/// <summary>
/// CrossEx business type
/// </summary>
public enum GateCrossExBusinessType
{
    [Map("SPOT")]
    Spot = 1,

    [Map("FUTURE")]
    Future = 2,

    [Map("MARGIN")]
    Margin = 3,
}

/// <summary>
/// CrossEx order side
/// </summary>
public enum GateCrossExOrderSide
{
    [Map("BUY")]
    Buy = 1,

    [Map("SELL")]
    Sell = 2,
}

/// <summary>
/// CrossEx order type
/// </summary>
public enum GateCrossExOrderType
{
    [Map("LIMIT")]
    Limit = 1,

    [Map("MARKET")]
    Market = 2,
}

/// <summary>
/// CrossEx time in force
/// </summary>
public enum GateCrossExTimeInForce
{
    [Map("GTC")]
    GoodTillCancelled = 1,

    [Map("IOC")]
    ImmediateOrCancelled = 2,

    [Map("FOK")]
    FillOrKill = 3,

    [Map("POC")]
    PendingOrCancelled = 4,
}

/// <summary>
/// CrossEx position side
/// </summary>
public enum GateCrossExPositionSide
{
    [Map("NONE")]
    None = 0,

    [Map("LONG")]
    Long = 1,

    [Map("SHORT")]
    Short = 2,
}

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

/// <summary>
/// CrossEx account mode
/// </summary>
public enum GateCrossExAccountMode
{
    [Map("CROSS_EXCHANGE")]
    CrossExchange = 1,

    [Map("ISOLATED_EXCHANGE")]
    IsolatedExchange = 2,
}

/// <summary>
/// CrossEx position mode
/// </summary>
public enum GateCrossExPositionMode
{
    [Map("SINGLE")]
    Single = 1,

    [Map("DUAL")]
    Dual = 2,
}
