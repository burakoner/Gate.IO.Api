namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment settlement currency.
/// </summary>
public enum GateEarnDualQuoteCurrency : byte
{
    /// <summary>
    /// Tether.
    /// </summary>
    [Map("USDT")]
    USDT = 1,

    /// <summary>
    /// Gate USD.
    /// </summary>
    [Map("GUSD")]
    GUSD = 2,
}
