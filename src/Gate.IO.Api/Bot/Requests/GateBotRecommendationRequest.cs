namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot recommendation request
/// </summary>
public record GateBotRecommendationRequest : GateBotRequestHeaders
{
    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Strategy Type.
    /// </summary>
    public GateBotStrategyType? StrategyType { get; set; }

    /// <summary>
    /// Gets or sets the Direction.
    /// </summary>
    public GateBotRecommendationDirection? Direction { get; set; }

    /// <summary>
    /// Gets or sets the Invest Amount.
    /// </summary>
    public decimal? InvestAmount { get; set; }

    /// <summary>
    /// Gets or sets the Scene.
    /// </summary>
    public GateBotDiscoverScene? Scene { get; set; }

    /// <summary>
    /// Gets or sets the Refresh Recommendation ID.
    /// </summary>
    public string RefreshRecommendationId { get; set; }

    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Gets or sets the Max Drawdown Less Than Or Equal.
    /// </summary>
    public decimal? MaxDrawdownLessThanOrEqual { get; set; }

    /// <summary>
    /// Gets or sets the Backtest APR Greater Than Or Equal.
    /// </summary>
    public decimal? BacktestAprGreaterThanOrEqual { get; set; }
}
