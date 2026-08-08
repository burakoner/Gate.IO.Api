namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx exchange type
/// </summary>
public enum GateCrossExExchangeType
{
    /// <summary>
    /// Represents the Binance value.
    /// </summary>
    [Map("BINANCE")]
    Binance = 1,

    /// <summary>
    /// Represents the Okx value.
    /// </summary>
    [Map("OKX")]
    Okx = 2,

    /// <summary>
    /// Represents the Gate value.
    /// </summary>
    [Map("GATE")]
    Gate = 3,

    /// <summary>
    /// Represents the Bybit value.
    /// </summary>
    [Map("BYBIT")]
    Bybit = 4,

    /// <summary>
    /// Represents the Cross Ex value.
    /// </summary>
    [Map("CROSSEX")]
    CrossEx = 5,

    /// <summary>
    /// Represents the Kraken value.
    /// </summary>
    [Map("KRAKEN")]
    Kraken = 6,

    /// <summary>
    /// Represents the Hyperliquid value.
    /// </summary>
    [Map("HYPERLIQUID")]
    Hyperliquid = 7,

    /// <summary>
    /// Represents the Deribit value.
    /// </summary>
    [Map("DERIBIT")]
    Deribit = 8,
}
