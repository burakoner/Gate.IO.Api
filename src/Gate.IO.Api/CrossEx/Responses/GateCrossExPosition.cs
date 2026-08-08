namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx contract position
/// </summary>
public record GateCrossExPosition
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the Position ID.
    /// </summary>
    [JsonProperty("position_id")]
    public string PositionId { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Position Side.
    /// </summary>
    [JsonProperty("position_side")]
    public string PositionSide { get; set; }

    /// <summary>
    /// Gets or sets the Initial Margin.
    /// </summary>
    [JsonProperty("initial_margin")]
    public decimal? InitialMargin { get; set; }

    /// <summary>
    /// Gets or sets the Maintenance Margin.
    /// </summary>
    [JsonProperty("maintenance_margin")]
    public decimal? MaintenanceMargin { get; set; }

    /// <summary>
    /// Gets or sets the Position Quantity.
    /// </summary>
    [JsonProperty("position_qty")]
    public decimal? PositionQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Position Value.
    /// </summary>
    [JsonProperty("position_value")]
    public decimal? PositionValue { get; set; }

    /// <summary>
    /// Gets or sets the Unrealized PnL.
    /// </summary>
    [JsonProperty("upnl")]
    public decimal? UnrealizedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Unrealized PnL Rate.
    /// </summary>
    [JsonProperty("upnl_rate")]
    public decimal? UnrealizedPnlRate { get; set; }

    /// <summary>
    /// Gets or sets the Entry Price.
    /// </summary>
    [JsonProperty("entry_price")]
    public decimal? EntryPrice { get; set; }

    [JsonProperty("avg_price")]
    private decimal? AveragePrice { set => EntryPrice = value; }

    /// <summary>
    /// Gets or sets the Mark Price.
    /// </summary>
    [JsonProperty("mark_price")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Gets or sets the Leverage.
    /// </summary>
    [JsonProperty("leverage")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Leverage.
    /// </summary>
    [JsonProperty("max_leverage")]
    public decimal? MaximumLeverage { get; set; }

    /// <summary>
    /// Gets or sets the Risk Limit.
    /// </summary>
    [JsonProperty("risk_limit")]
    public decimal? RiskLimit { get; set; }

    /// <summary>
    /// Gets or sets the Fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Gets or sets the accumulated position funding fee. A positive value indicates a gain; a negative value indicates a loss.
    /// </summary>
    [JsonProperty("funding_fee")]
    public decimal? FundingFee { get; set; }

    /// <summary>
    /// Gets or sets the position funding-fee collection time. A wire value of zero means no fee has been collected and maps to null.
    /// </summary>
    [JsonProperty("funding_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FundingTime { get; set; }

    /// <summary>
    /// Gets or sets the Create Time.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// Gets or sets the Update Time.
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// Gets or sets the Closed PnL.
    /// </summary>
    [JsonProperty("closed_pnl")]
    public decimal? ClosedPnl { get; set; }
}
