namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy base information
/// </summary>
public record GateBotPortfolioBaseInfo
{
    [JsonProperty("strategy_name")]
    public string StrategyName { get; set; }

    [JsonProperty("created_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("running_duration")]
    public long? RunningDuration { get; set; }

    [JsonProperty("invest_amount")]
    public decimal? InvestAmount { get; set; }

    [JsonProperty("total_profit")]
    public decimal? TotalProfit { get; set; }

    [JsonProperty("profit_rate")]
    public decimal? ProfitRate { get; set; }
}
