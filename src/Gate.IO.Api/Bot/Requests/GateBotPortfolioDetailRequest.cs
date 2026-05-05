namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy detail request
/// </summary>
public record GateBotPortfolioDetailRequest : GateBotRequestHeaders
{
    /// <summary>
    /// Gets or sets the Strategy ID.
    /// </summary>
    public string StrategyId { get; set; }

    /// <summary>
    /// Gets or sets the Strategy Type.
    /// </summary>
    public GateBotStrategyType StrategyType { get; set; }
}
