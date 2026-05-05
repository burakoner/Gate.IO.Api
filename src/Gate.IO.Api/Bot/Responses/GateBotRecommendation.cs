namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy recommendation
/// </summary>
public record GateBotRecommendation
{
    /// <summary>
    /// Gets or sets the Recommendation ID.
    /// </summary>
    [JsonProperty("recommendation_id")]
    public string RecommendationId { get; set; }

    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    [JsonProperty("market")]
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Strategy Type.
    /// </summary>
    [JsonProperty("strategy_type"), JsonConverter(typeof(MapConverter))]
    public GateBotStrategyType? StrategyType { get; set; }

    /// <summary>
    /// Gets or sets the Strategy Name.
    /// </summary>
    [JsonProperty("strategy_name")]
    public string StrategyName { get; set; }

    /// <summary>
    /// Gets or sets the Backtest APR.
    /// </summary>
    [JsonProperty("backtest_apr")]
    public decimal? BacktestApr { get; set; }

    /// <summary>
    /// Gets or sets the Max Drawdown.
    /// </summary>
    [JsonProperty("max_drawdown")]
    public decimal? MaxDrawdown { get; set; }

    /// <summary>
    /// Gets or sets the Summary.
    /// </summary>
    [JsonProperty("summary")]
    public string Summary { get; set; }

    /// <summary>
    /// Gets or sets the Strategy Parameters Preview.
    /// </summary>
    [JsonProperty("strategy_params_preview")]
    public JObject StrategyParametersPreview { get; set; }
}
