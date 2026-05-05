namespace Gate.IO.Api.Bot;

/// <summary>
/// Running bot strategy
/// </summary>
public record GateBotRunningStrategy
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
    /// Gets or sets the Strategy Name.
    /// </summary>
    [JsonProperty("strategy_name")]
    public string StrategyName { get; set; }

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
    /// Gets or sets the PnL.
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? Pnl { get; set; }

    /// <summary>
    /// Gets or sets the PnL Rate.
    /// </summary>
    [JsonProperty("pnl_rate")]
    public decimal? PnlRate { get; set; }

    /// <summary>
    /// Gets or sets the Invest Amount.
    /// </summary>
    [JsonProperty("invest_amount")]
    public decimal? InvestAmount { get; set; }

    /// <summary>
    /// Gets or sets the Created At.
    /// </summary>
    [JsonProperty("created_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreatedAt { get; set; }
}
