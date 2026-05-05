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
}
