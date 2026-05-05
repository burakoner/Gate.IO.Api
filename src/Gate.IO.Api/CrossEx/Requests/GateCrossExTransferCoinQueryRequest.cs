namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx transfer currency query request
/// </summary>
public record GateCrossExTransferCoinQueryRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Coin { get; set; }
}
