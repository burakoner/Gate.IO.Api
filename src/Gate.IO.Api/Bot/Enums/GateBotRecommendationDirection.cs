namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot recommendation market direction
/// </summary>
public enum GateBotRecommendationDirection : byte
{
    [Map("buy")]
    Buy = 1,

    [Map("sell")]
    Sell = 2,

    [Map("neutral")]
    Neutral = 3,
}
