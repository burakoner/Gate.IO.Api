namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx coin and exchange query request
/// </summary>
public record GateCrossExCoinExchangeQueryRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Exchange type
    /// </summary>
    public GateCrossExExchangeType? ExchangeType { get; set; }
}
