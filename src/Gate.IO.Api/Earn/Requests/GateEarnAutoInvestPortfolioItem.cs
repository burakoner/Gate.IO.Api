namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest portfolio item
/// </summary>
public record GateEarnAutoInvestPortfolioItem
{
    /// <summary>
    /// Investment currency
    /// </summary>
    public string Asset { get; set; }

    /// <summary>
    /// Portfolio ratio
    /// </summary>
    public decimal Ratio { get; set; }
}
