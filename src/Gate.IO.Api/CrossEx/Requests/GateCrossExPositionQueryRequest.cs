namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx position query request
/// </summary>
public record GateCrossExPositionQueryRequest
{
    /// <summary>
    /// Trading pair
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Exchange type
    /// </summary>
    public GateCrossExExchangeType? ExchangeType { get; set; }
}
