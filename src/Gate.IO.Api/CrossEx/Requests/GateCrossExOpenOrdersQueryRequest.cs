namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx open orders query request
/// </summary>
public record GateCrossExOpenOrdersQueryRequest
{
    /// <summary>
    /// Trading pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Exchange type
    /// </summary>
    public GateCrossExExchangeType? ExchangeType { get; set; }

    /// <summary>
    /// Business type
    /// </summary>
    public GateCrossExBusinessType? BusinessType { get; set; }
}
