namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot recommendation request
/// </summary>
public record GateBotRecommendationRequest : GateBotRequestHeaders
{
    public string Market { get; set; }

    public GateBotStrategyType? StrategyType { get; set; }

    public GateBotRecommendationDirection? Direction { get; set; }

    public decimal? InvestAmount { get; set; }

    public GateBotDiscoverScene? Scene { get; set; }

    public string RefreshRecommendationId { get; set; }

    public int? Limit { get; set; }

    public decimal? MaxDrawdownLessThanOrEqual { get; set; }

    public decimal? BacktestAprGreaterThanOrEqual { get; set; }
}
