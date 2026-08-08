namespace Gate.IO.Api.Otc;

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
}
