namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx historical contract position
/// </summary>
public record GateCrossExHistoricalPosition
{
    /// <summary>
    /// Gets or sets the Position ID.
    /// </summary>
    [JsonProperty("position_id")]
    public long? PositionId { get; set; }

    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the Symbol.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Closed Type.
    /// </summary>
    [JsonProperty("closed_type")]
    public string ClosedType { get; set; }

    /// <summary>
    /// Gets or sets the Closed PnL.
    /// </summary>
    [JsonProperty("closed_pnl")]
    public decimal? ClosedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Closed PnL Rate.
    /// </summary>
    [JsonProperty("closed_pnl_rate")]
    public decimal? ClosedPnlRate { get; set; }

    /// <summary>
    /// Gets or sets the Open Average Price.
    /// </summary>
    [JsonProperty("open_avg_price")]
    public decimal? OpenAveragePrice { get; set; }

    /// <summary>
    /// Gets or sets the Closed Average Price.
    /// </summary>
    [JsonProperty("closed_avg_price")]
    public decimal? ClosedAveragePrice { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Position Quantity.
    /// </summary>
    [JsonProperty("max_position_qty")]
    public decimal? MaximumPositionQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Closed Quantity.
    /// </summary>
    [JsonProperty("closed_qty")]
    public decimal? ClosedQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Closed Value.
    /// </summary>
    [JsonProperty("closed_value")]
    public decimal? ClosedValue { get; set; }

    /// <summary>
    /// Gets or sets the Fee.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Gets or sets the Liquidation Fee.
    /// </summary>
    [JsonProperty("liq_fee")]
    public decimal? LiquidationFee { get; set; }

    /// <summary>
    /// Gets or sets the Funding Fee.
    /// </summary>
    [JsonProperty("funding_fee")]
    public decimal? FundingFee { get; set; }

    /// <summary>
    /// Gets or sets the Position Side.
    /// </summary>
    [JsonProperty("position_side")]
    public string PositionSide { get; set; }

    /// <summary>
    /// Gets or sets the Position Mode.
    /// </summary>
    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    /// <summary>
    /// Gets or sets the Leverage.
    /// </summary>
    [JsonProperty("leverage")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Gets or sets the Business Type.
    /// </summary>
    [JsonProperty("business_type")]
    public string BusinessType { get; set; }

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
}
