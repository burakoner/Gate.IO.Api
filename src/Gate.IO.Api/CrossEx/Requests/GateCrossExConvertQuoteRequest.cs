namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx flash swap quote request
/// </summary>
public record GateCrossExConvertQuoteRequest
{
    /// <summary>
    /// Exchange type
    /// </summary>
    public GateCrossExExchangeType ExchangeType { get; set; }

    /// <summary>
    /// Asset sold
    /// </summary>
    public string FromCoin { get; set; }

    /// <summary>
    /// Asset bought
    /// </summary>
    public string ToCoin { get; set; }

    /// <summary>
    /// Amount to sell
    /// </summary>
    public decimal FromAmount { get; set; }
}
