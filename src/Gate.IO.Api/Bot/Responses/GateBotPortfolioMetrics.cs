namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy metrics
/// </summary>
public record GateBotPortfolioMetrics
{
    [JsonProperty("grid_profit")]
    public decimal? GridProfit { get; set; }

    [JsonProperty("floating_pnl")]
    public decimal? FloatingPnl { get; set; }

    [JsonProperty("arbitrage_count")]
    public int? ArbitrageCount { get; set; }

    [JsonProperty("price_range")]
    public string PriceRange { get; set; }

    [JsonProperty("grid_count")]
    public int? GridCount { get; set; }

    [JsonProperty("estimated_liquidation_price")]
    public decimal? EstimatedLiquidationPrice { get; set; }

    [JsonProperty("price_floor")]
    public decimal? PriceFloor { get; set; }

    [JsonProperty("grid_profit_rate")]
    public decimal? GridProfitRate { get; set; }

    [JsonProperty("realized_pnl")]
    public decimal? RealizedPnl { get; set; }

    [JsonProperty("finished_rounds")]
    public int? FinishedRounds { get; set; }

    [JsonProperty("avg_cost")]
    public decimal? AverageCost { get; set; }

    [JsonProperty("take_profit_price")]
    public decimal? TakeProfitPrice { get; set; }

    [JsonProperty("maintenance_margin_ratio")]
    public decimal? MaintenanceMarginRatio { get; set; }
}
