namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy detail request
/// </summary>
public record GateBotPortfolioDetailRequest : GateBotRequestHeaders
{
    public string StrategyId { get; set; }

    public GateBotStrategyType StrategyType { get; set; }
}
