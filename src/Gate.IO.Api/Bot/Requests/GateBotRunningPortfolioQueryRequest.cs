namespace Gate.IO.Api.Bot;

/// <summary>
/// Running bot strategy query request
/// </summary>
public record GateBotRunningPortfolioQueryRequest : GateBotRequestHeaders
{
    /// <summary>
    /// Gets or sets the Strategy Type.
    /// </summary>
    public GateBotStrategyType? StrategyType { get; set; }

    /// <summary>
    /// Gets or sets the Market.
    /// </summary>
    public string Market { get; set; }

    /// <summary>
    /// Gets or sets the Page.
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Gets or sets the Page Size.
    /// </summary>
    public int? PageSize { get; set; }
}
