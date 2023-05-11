namespace Gate.IO.Api.Enums;

/// <summary>
/// Trade role. No value in public endpoints
/// </summary>
public enum OptionsTraderRole
{
    /// <summary>
    /// Enum Taker for value: taker
    /// </summary>
    [Label("taker")]
    Taker = 1,

    /// <summary>
    /// Enum Maker for value: maker
    /// </summary>
    [Label("maker")]
    Maker = 2
}
