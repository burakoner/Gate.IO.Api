namespace Gate.IO.Api.Bot;

/// <summary>
/// Running bot strategy query request
/// </summary>
public record GateBotRunningPortfolioQueryRequest : GateBotRequestHeaders
{
    public GateBotStrategyType? StrategyType { get; set; }

    public string Market { get; set; }

    public int? Page { get; set; }

    public int? PageSize { get; set; }
}
