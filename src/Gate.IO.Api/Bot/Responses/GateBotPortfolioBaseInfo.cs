namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy base information
/// </summary>
public record GateBotPortfolioBaseInfo
{
    /// <summary>
    /// Gets or sets the Strategy Name.
    /// </summary>
    [JsonProperty("strategy_name")]
    public string StrategyName { get; set; }

    /// <summary>
    /// Gets or sets the Created At.
    /// </summary>
    [JsonProperty("created_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the Running Duration.
    /// </summary>
    [JsonProperty("running_duration")]
    public long? RunningDuration { get; set; }

    /// <summary>
    /// Gets or sets the Invest Amount.
    /// </summary>
    [JsonProperty("invest_amount")]
    public decimal? InvestAmount { get; set; }

    /// <summary>
    /// Gets or sets the Total Profit.
    /// </summary>
    [JsonProperty("total_profit")]
    public decimal? TotalProfit { get; set; }

    /// <summary>
    /// Gets or sets the Profit Rate.
    /// </summary>
    [JsonProperty("profit_rate")]
    public decimal? ProfitRate { get; set; }
}
