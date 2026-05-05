namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy detail
/// </summary>
public record GateBotPortfolioDetail
{
    [JsonProperty("strategy_id")]
    public string StrategyId { get; set; }

    [JsonProperty("strategy_type"), JsonConverter(typeof(MapConverter))]
    public GateBotStrategyType? StrategyType { get; set; }

    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("base_info")]
    public GateBotPortfolioBaseInfo BaseInfo { get; set; }

    [JsonProperty("metrics")]
    public GateBotPortfolioMetrics Metrics { get; set; }

    [JsonProperty("position")]
    public GateBotPortfolioPosition Position { get; set; }

    [JsonProperty("stop_supported")]
    public bool StopSupported { get; set; }
}
