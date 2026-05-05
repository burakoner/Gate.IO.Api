namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx contract position
/// </summary>
public record GateCrossExPosition
{
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    [JsonProperty("position_id")]
    public long? PositionId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("position_side")]
    public string PositionSide { get; set; }

    [JsonProperty("initial_margin")]
    public decimal? InitialMargin { get; set; }

    [JsonProperty("maintenance_margin")]
    public decimal? MaintenanceMargin { get; set; }

    [JsonProperty("position_qty")]
    public decimal? PositionQuantity { get; set; }

    [JsonProperty("position_value")]
    public decimal? PositionValue { get; set; }

    [JsonProperty("upnl")]
    public decimal? UnrealizedPnl { get; set; }

    [JsonProperty("upnl_rate")]
    public decimal? UnrealizedPnlRate { get; set; }

    [JsonProperty("entry_price")]
    public decimal? EntryPrice { get; set; }

    [JsonProperty("avg_price")]
    private decimal? AveragePrice { set => EntryPrice = value; }

    [JsonProperty("mark_price")]
    public decimal? MarkPrice { get; set; }

    [JsonProperty("leverage")]
    public decimal? Leverage { get; set; }

    [JsonProperty("max_leverage")]
    public decimal? MaximumLeverage { get; set; }

    [JsonProperty("risk_limit")]
    public decimal? RiskLimit { get; set; }

    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    [JsonProperty("funding_fee")]
    public decimal? FundingFee { get; set; }

    [JsonProperty("funding_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FundingTime { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }

    [JsonProperty("closed_pnl")]
    public decimal? ClosedPnl { get; set; }
}
