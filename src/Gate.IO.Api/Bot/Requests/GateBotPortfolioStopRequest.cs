namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy stop request
/// </summary>
public record GateBotPortfolioStopRequest : GateBotRequestHeaders
{
    public string StrategyId { get; set; }

    public GateBotStrategyType StrategyType { get; set; }
}
