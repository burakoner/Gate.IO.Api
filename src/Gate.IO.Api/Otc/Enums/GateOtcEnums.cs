namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC quote direction
/// </summary>
public enum GateOtcQuoteSide : byte
{
    /// <summary>
    /// User inputs the payment amount
    /// </summary>
    [Map("PAY")]
    Pay = 1,

    /// <summary>
    /// User inputs the received amount
    /// </summary>
    [Map("GET")]
    Get = 2,
}

/// <summary>
/// OTC order type
/// </summary>
public enum GateOtcOrderType : byte
{
    /// <summary>
    /// Buy, on-ramp
    /// </summary>
    [Map("BUY")]
    Buy = 1,

    /// <summary>
    /// Sell, off-ramp
    /// </summary>
    [Map("SELL")]
    Sell = 2,

    /// <summary>
    /// All orders
    /// </summary>
    [Map("ALL")]
    All = 3,
}

/// <summary>
/// OTC order kind
/// </summary>
public enum GateOtcOrderKind : byte
{
    /// <summary>
    /// Fiat order
    /// </summary>
    [Map("FIAT")]
    Fiat = 1,

    /// <summary>
    /// Stablecoin order
    /// </summary>
    [Map("STABLE")]
    Stable = 2,
}
