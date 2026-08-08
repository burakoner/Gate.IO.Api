namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock exchange
/// </summary>
public enum GateStockExchange
{
    /// <summary>United States exchanges</summary>
    [Map("us")]
    UnitedStates,
    /// <summary>Hong Kong exchange</summary>
    [Map("hk")]
    HongKong,
    /// <summary>South Korea exchange</summary>
    [Map("kr")]
    SouthKorea,
}
