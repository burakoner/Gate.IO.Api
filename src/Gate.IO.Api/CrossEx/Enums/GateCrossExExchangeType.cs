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
}
