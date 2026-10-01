namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx historical contract position
/// </summary>
public record GateCrossExHistoricalPosition
{
    /// <summary>
    /// Exact Int64 position identity, including numeric strings. Missing/null is unknown; fractional/overflow values are rejected.
    /// </summary>
    [JsonProperty("position_id")]
    [JsonConverter(typeof(GateFuturesOrderIdConverter))]
    public long? PositionId { get; set; }

    /// <summary>
    /// Exact Int64 user identity, including numeric strings. Missing/null is unknown; fractional/overflow values are rejected.
    /// </summary>
    [JsonProperty("user_id")]
    [JsonConverter(typeof(GateFuturesOrderIdConverter))]
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
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? ClosedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Closed PnL Rate.
    /// </summary>
    [JsonProperty("closed_pnl_rate")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? ClosedPnlRate { get; set; }

    /// <summary>
    /// Gets or sets the Open Average Price.
    /// </summary>
    [JsonProperty("open_avg_price")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? OpenAveragePrice { get; set; }

    /// <summary>
    /// Gets or sets the Closed Average Price.
    /// </summary>
    [JsonProperty("closed_avg_price")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? ClosedAveragePrice { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Position Quantity.
    /// </summary>
    [JsonProperty("max_position_qty")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? MaximumPositionQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Closed Quantity.
    /// </summary>
    [JsonProperty("closed_qty")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? ClosedQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Closed Value.
    /// </summary>
    [JsonProperty("closed_value")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? ClosedValue { get; set; }

    /// <summary>
    /// Gets or sets the Fee.
    /// </summary>
    [JsonProperty("fee")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Gets or sets the Liquidation Fee.
    /// </summary>
    [JsonProperty("liq_fee")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? LiquidationFee { get; set; }

    /// <summary>
    /// Gets or sets the Funding Fee.
    /// </summary>
    [JsonProperty("funding_fee")]
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
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
    [JsonConverter(typeof(GateFuturesOrderDecimalStringConverter))]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Margin mode, currently CROSS or ISOLATED. Omission is unknown; unfamiliar values are retained without interpretation.
    /// </summary>
    [JsonProperty("margin_mode")]
    public string MarginMode { get; set; }

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
