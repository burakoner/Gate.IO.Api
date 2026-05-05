namespace Gate.IO.Api.Earn;

/// <summary>
/// Auto invest minimum amount request
/// </summary>
public record GateEarnAutoInvestMinInvestAmountRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Money { get; set; }

    /// <summary>
    /// Investment portfolio
    /// </summary>
    public IEnumerable<GateEarnAutoInvestPortfolioItem> Items { get; set; }
}
