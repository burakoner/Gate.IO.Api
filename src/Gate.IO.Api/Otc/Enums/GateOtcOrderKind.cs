namespace Gate.IO.Api.Otc;

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

    /// <summary>Legacy fiat-order quote validation side CRYPTO.</summary>
    [Map("CRYPTO")]
    Crypto = 3,

    /// <summary>Payment-amount quote side. Use the side returned by the quote.</summary>
    [Map("PAY")]
    Pay = 4,

    /// <summary>Receive-amount quote side. Use the side returned by the quote.</summary>
    [Map("GET")]
    Get = 5,
}
