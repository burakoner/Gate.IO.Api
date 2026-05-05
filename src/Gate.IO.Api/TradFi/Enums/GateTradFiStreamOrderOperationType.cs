namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi stream order operation type.
/// </summary>
public enum GateTradFiStreamOrderOperationType
{
    /// <summary>
    /// Sell operation.
    /// </summary>
    Sell = 1,

    /// <summary>
    /// Buy operation.
    /// </summary>
    Buy = 2,

    /// <summary>
    /// Close long operation.
    /// </summary>
    CloseLong = 3,

    /// <summary>
    /// Close short operation.
    /// </summary>
    CloseShort = 4,

    /// <summary>
    /// Liquidates a long position.
    /// </summary>
    LiquidatesLong = 5,

    /// <summary>
    /// Liquidates a short position.
    /// </summary>
    LiquidatesShort = 6,
}
