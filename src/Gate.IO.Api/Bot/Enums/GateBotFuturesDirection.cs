namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot futures-style strategy direction
/// </summary>
public enum GateBotFuturesDirection : byte
{
    /// <summary>
    /// Represents the Long value.
    /// </summary>
    [Map("long")]
    Long = 1,

    /// <summary>
    /// Represents the Short value.
    /// </summary>
    [Map("short")]
    Short = 2,

    /// <summary>
    /// Represents the Neutral value.
    /// </summary>
    [Map("neutral")]
    Neutral = 3,
}
