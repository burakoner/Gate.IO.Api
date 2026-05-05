namespace Gate.IO.Api.Rebate;

/// <summary>
/// Rebate partner business type
/// </summary>
public enum GateRebateBusinessType : byte
{
    /// <summary>
    /// All
    /// </summary>
    All = 0,

    /// <summary>
    /// Spot
    /// </summary>
    Spot = 1,

    /// <summary>
    /// Futures
    /// </summary>
    Futures = 2,

    /// <summary>
    /// Alpha
    /// </summary>
    Alpha = 3,

    /// <summary>
    /// Web3
    /// </summary>
    Web3 = 4,

    /// <summary>
    /// Perps (DEX)
    /// </summary>
    PerpsDex = 5,

    /// <summary>
    /// Exchange All
    /// </summary>
    ExchangeAll = 6,

    /// <summary>
    /// Web3 All
    /// </summary>
    Web3All = 7,

    /// <summary>
    /// TradFi
    /// </summary>
    TradFi = 8,
}
