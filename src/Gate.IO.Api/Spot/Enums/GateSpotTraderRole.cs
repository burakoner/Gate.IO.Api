namespace Gate.IO.Api.Spot;

/// <summary>
/// Trade role. No value in public endpoints
/// </summary>
public enum GateSpotTraderRole
{
    /// <summary>
    /// Enum Taker for value: taker
    /// </summary>
    [Map("taker")]
    Taker,

    /// <summary>
    /// Enum Maker for value: maker
    /// </summary>
    [Map("maker")]
    Maker
}
