namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot recommendation result
/// </summary>
public record GateBotRecommendationResult
{
    [JsonProperty("scene"), JsonConverter(typeof(MapConverter))]
    public GateBotDiscoverScene? Scene { get; set; }

    [JsonProperty("recommendations")]
    public List<GateBotRecommendation> Recommendations { get; set; } = [];

    [JsonProperty("unsupported_filters")]
    public List<string> UnsupportedFilters { get; set; } = [];
}
