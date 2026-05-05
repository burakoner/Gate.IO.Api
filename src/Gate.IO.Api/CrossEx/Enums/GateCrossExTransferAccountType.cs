namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account type for transfers
/// </summary>
public enum GateCrossExTransferAccountType
{
    /// <summary>
    /// Represents the Cross Ex Binance value.
    /// </summary>
    [Map("CROSSEX_BINANCE")]
    CrossExBinance = 1,

    /// <summary>
    /// Represents the Cross Ex Okx value.
    /// </summary>
    [Map("CROSSEX_OKX")]
    CrossExOkx = 2,

    /// <summary>
    /// Represents the Cross Ex Gate value.
    /// </summary>
    [Map("CROSSEX_GATE")]
    CrossExGate = 3,

    /// <summary>
    /// Represents the Cross Ex Bybit value.
    /// </summary>
    [Map("CROSSEX_BYBIT")]
    CrossExBybit = 4,

    /// <summary>
    /// Represents the Cross Ex value.
    /// </summary>
    [Map("CROSSEX")]
    CrossEx = 5,

    /// <summary>
    /// Represents the Spot value.
    /// </summary>
    [Map("SPOT")]
    Spot = 6,
}
