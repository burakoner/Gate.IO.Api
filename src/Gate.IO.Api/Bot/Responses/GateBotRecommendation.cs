namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy recommendation
/// </summary>
public record GateBotRecommendation
{
    [JsonProperty("recommendation_id")]
    public string RecommendationId { get; set; }

    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("strategy_type"), JsonConverter(typeof(MapConverter))]
    public GateBotStrategyType? StrategyType { get; set; }

    [JsonProperty("strategy_name")]
    public string StrategyName { get; set; }

    [JsonProperty("backtest_apr")]
    public decimal? BacktestApr { get; set; }

    [JsonProperty("max_drawdown")]
    public decimal? MaxDrawdown { get; set; }

    [JsonProperty("summary")]
    public string Summary { get; set; }

    [JsonProperty("strategy_params_preview")]
    public JObject StrategyParametersPreview { get; set; }
}
