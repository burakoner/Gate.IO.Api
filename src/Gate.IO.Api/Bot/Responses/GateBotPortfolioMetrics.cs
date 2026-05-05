namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy metrics
/// </summary>
public record GateBotPortfolioMetrics
{
    /// <summary>
    /// Gets or sets the Grid Profit.
    /// </summary>
    [JsonProperty("grid_profit")]
    public decimal? GridProfit { get; set; }

    /// <summary>
    /// Gets or sets the Floating PnL.
    /// </summary>
    [JsonProperty("floating_pnl")]
    public decimal? FloatingPnl { get; set; }

    /// <summary>
    /// Gets or sets the Arbitrage Count.
    /// </summary>
    [JsonProperty("arbitrage_count")]
    public int? ArbitrageCount { get; set; }

    /// <summary>
    /// Gets or sets the Price Range.
    /// </summary>
    [JsonProperty("price_range")]
    public string PriceRange { get; set; }

    /// <summary>
    /// Gets or sets the Grid Count.
    /// </summary>
    [JsonProperty("grid_count")]
    public int? GridCount { get; set; }

    /// <summary>
    /// Gets or sets the Estimated Liquidation Price.
    /// </summary>
    [JsonProperty("estimated_liquidation_price")]
    public decimal? EstimatedLiquidationPrice { get; set; }

    /// <summary>
    /// Gets or sets the Price Floor.
    /// </summary>
    [JsonProperty("price_floor")]
    public decimal? PriceFloor { get; set; }

    /// <summary>
    /// Gets or sets the Grid Profit Rate.
    /// </summary>
    [JsonProperty("grid_profit_rate")]
    public decimal? GridProfitRate { get; set; }

    /// <summary>
    /// Gets or sets the Realized PnL.
    /// </summary>
    [JsonProperty("realized_pnl")]
    public decimal? RealizedPnl { get; set; }

    /// <summary>
    /// Gets or sets the Finished Rounds.
    /// </summary>
    [JsonProperty("finished_rounds")]
    public int? FinishedRounds { get; set; }

    /// <summary>
    /// Gets or sets the Average Cost.
    /// </summary>
    [JsonProperty("avg_cost")]
    public decimal? AverageCost { get; set; }

    /// <summary>
    /// Gets or sets the Take Profit Price.
    /// </summary>
    [JsonProperty("take_profit_price")]
    public decimal? TakeProfitPrice { get; set; }

    /// <summary>
    /// Gets or sets the Maintenance Margin Ratio.
    /// </summary>
    [JsonProperty("maintenance_margin_ratio")]
    public decimal? MaintenanceMarginRatio { get; set; }
}
