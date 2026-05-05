namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot futures-style strategy direction
/// </summary>
public enum GateBotFuturesDirection : byte
{
    [Map("long")]
    Long = 1,

    [Map("short")]
    Short = 2,

    [Map("neutral")]
    Neutral = 3,
}
