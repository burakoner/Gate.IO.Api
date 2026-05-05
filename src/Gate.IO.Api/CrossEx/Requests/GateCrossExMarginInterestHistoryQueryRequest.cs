namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx margin interest history query request
/// </summary>
public record GateCrossExMarginInterestHistoryQueryRequest : GateCrossExHistoryQueryRequest
{
    /// <summary>
    /// Exchange type
    /// </summary>
    public GateCrossExExchangeType? ExchangeType { get; set; }
}
