namespace Gate.IO.Api.Bot;

/// <summary>
/// Running bot strategy
/// </summary>
public record GateBotRunningStrategy
{
    [JsonProperty("strategy_id")]
    public string StrategyId { get; set; }

    [JsonProperty("strategy_type"), JsonConverter(typeof(MapConverter))]
    public GateBotStrategyType? StrategyType { get; set; }

    [JsonProperty("strategy_name")]
    public string StrategyName { get; set; }

    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("pnl")]
    public decimal? Pnl { get; set; }

    [JsonProperty("pnl_rate")]
    public decimal? PnlRate { get; set; }

    [JsonProperty("invest_amount")]
    public decimal? InvestAmount { get; set; }

    [JsonProperty("created_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreatedAt { get; set; }
}
