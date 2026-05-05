namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx account query request
/// </summary>
public record GateCrossExAccountQueryRequest
{
    /// <summary>
    /// Exchange type
    /// </summary>
    public GateCrossExExchangeType? ExchangeType { get; set; }
}
