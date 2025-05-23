namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Asset Class
/// </summary>
public enum GateWalletAssetClass : byte
{
    /// <summary>
    /// Spot, Main Zone
    /// </summary>
    [Map("SPOT")]
    MainZone = 1,

    /// <summary>
    /// Pilot, Innovation Zone
    /// </summary>
    [Map("PILOT")]
    InnovationZone = 2,
}
