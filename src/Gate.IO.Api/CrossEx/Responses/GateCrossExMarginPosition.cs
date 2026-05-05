namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx margin position
/// </summary>
public record GateCrossExMarginPosition
{
    /// <summary>
    /// Gets or sets the User ID.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; set; }

    /// <summary>
    /// Gets or sets the Position ID.
    /// </summary>
    [JsonProperty("position_id")]
    public long? PositionId { get; set; }

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
    /// Gets or sets the Asset Quantity.
    /// </summary>
    [JsonProperty("asset_qty")]
    public decimal? AssetQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Asset Coin.
    /// </summary>
    [JsonProperty("asset_coin")]
    public string AssetCoin { get; set; }

    /// <summary>
    /// Gets or sets the Position Value.
    /// </summary>
    [JsonProperty("position_value")]
    public decimal? PositionValue { get; set; }

    /// <summary>
    /// Gets or sets the Liability.
    /// </summary>
    [JsonProperty("liability")]
    public decimal? Liability { get; set; }

    /// <summary>
    /// Gets or sets the Liability Coin.
    /// </summary>
    [JsonProperty("liability_coin")]
    public string LiabilityCoin { get; set; }

    /// <summary>
    /// Gets or sets the Interest.
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Gets or sets the Maximum Position Quantity.
    /// </summary>
    [JsonProperty("max_position_qty")]
    public decimal? MaximumPositionQuantity { get; set; }

    /// <summary>
    /// Gets or sets the Entry Price.
    /// </summary>
    [JsonProperty("entry_price")]
    public decimal? EntryPrice { get; set; }

    /// <summary>
    /// Gets or sets the Index Price.
    /// </summary>
    [JsonProperty("index_price")]
    public decimal? IndexPrice { get; set; }

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
