namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot recommendation result
/// </summary>
public record GateBotRecommendationResult
{
    /// <summary>
    /// Gets or sets the Scene.
    /// </summary>
    [JsonProperty("scene"), JsonConverter(typeof(MapConverter))]
    public GateBotDiscoverScene? Scene { get; set; }

    /// <summary>
    /// Gets or sets the Recommendations.
    /// </summary>
    [JsonProperty("recommendations")]
    public List<GateBotRecommendation> Recommendations { get; set; } = [];

    /// <summary>
    /// Gets or sets the Unsupported Filters.
    /// </summary>
    [JsonProperty("unsupported_filters")]
    public List<string> UnsupportedFilters { get; set; } = [];
}
