namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx margin position
/// </summary>
public record GateCrossExMarginPosition
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

    [JsonProperty("asset_qty")]
    public decimal? AssetQuantity { get; set; }

    [JsonProperty("asset_coin")]
    public string AssetCoin { get; set; }

    [JsonProperty("position_value")]
    public decimal? PositionValue { get; set; }

    [JsonProperty("liability")]
    public decimal? Liability { get; set; }

    [JsonProperty("liability_coin")]
    public string LiabilityCoin { get; set; }

    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    [JsonProperty("max_position_qty")]
    public decimal? MaximumPositionQuantity { get; set; }

    [JsonProperty("entry_price")]
    public decimal? EntryPrice { get; set; }

    [JsonProperty("index_price")]
    public decimal? IndexPrice { get; set; }

    [JsonProperty("upnl")]
    public decimal? UnrealizedPnl { get; set; }

    [JsonProperty("upnl_rate")]
    public decimal? UnrealizedPnlRate { get; set; }

    [JsonProperty("leverage")]
    public decimal? Leverage { get; set; }

    [JsonProperty("max_leverage")]
    public decimal? MaximumLeverage { get; set; }

    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CreateTime { get; set; }

    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }
}
