namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot recommendation market direction
/// </summary>
public enum GateBotRecommendationDirection : byte
{
    /// <summary>
    /// Represents the Buy value.
    /// </summary>
    [Map("buy")]
    Buy = 1,

    /// <summary>
    /// Represents the Sell value.
    /// </summary>
    [Map("sell")]
    Sell = 2,

    /// <summary>
    /// Represents the Neutral value.
    /// </summary>
    [Map("neutral")]
    Neutral = 3,
}
