namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy detail
/// </summary>
public record GateBotPortfolioDetail
{
    /// <summary>
    /// Gets or sets the Strategy ID.
    /// </summary>
    [JsonProperty("strategy_id")]
    public string StrategyId { get; set; }

    /// <summary>
    /// Gets or sets the Strategy Type.
    /// </summary>
    [JsonProperty("strategy_type"), JsonConverter(typeof(MapConverter))]
    public GateBotStrategyType? StrategyType { get; set; }

    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    [JsonProperty("market")]
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the Base Info.
    /// </summary>
    [JsonProperty("base_info")]
    public GateBotPortfolioBaseInfo BaseInfo { get; set; }

    /// <summary>
    /// Gets or sets the Metrics.
    /// </summary>
    [JsonProperty("metrics")]
    public GateBotPortfolioMetrics Metrics { get; set; }

    /// <summary>
    /// Gets or sets the Position.
    /// </summary>
    [JsonProperty("position")]
    public GateBotPortfolioPosition Position { get; set; }

    /// <summary>
    /// Gets or sets the Stop Supported.
    /// </summary>
    [JsonProperty("stop_supported")]
    public bool StopSupported { get; set; }
}
