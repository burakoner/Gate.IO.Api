namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx historical contract position
/// </summary>
public record GateCrossExHistoricalPosition
{
    [JsonProperty("position_id")]
    public long? PositionId { get; set; }

    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("closed_type")]
    public string ClosedType { get; set; }

    [JsonProperty("closed_pnl")]
    public decimal? ClosedPnl { get; set; }

    [JsonProperty("closed_pnl_rate")]
    public decimal? ClosedPnlRate { get; set; }

    [JsonProperty("open_avg_price")]
    public decimal? OpenAveragePrice { get; set; }

    [JsonProperty("closed_avg_price")]
    public decimal? ClosedAveragePrice { get; set; }

    [JsonProperty("max_position_qty")]
    public decimal? MaximumPositionQuantity { get; set; }

    [JsonProperty("closed_qty")]
    public decimal? ClosedQuantity { get; set; }

    [JsonProperty("closed_value")]
    public decimal? ClosedValue { get; set; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("liq_fee")]
    public decimal? LiquidationFee { get; set; }

    [JsonProperty("funding_fee")]
    public decimal? FundingFee { get; set; }

    [JsonProperty("position_side")]
    public string PositionSide { get; set; }

    [JsonProperty("position_mode")]
    public string PositionMode { get; set; }

    [JsonProperty("leverage")]
    public decimal? Leverage { get; set; }

    [JsonProperty("business_type")]
    public string BusinessType { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}
