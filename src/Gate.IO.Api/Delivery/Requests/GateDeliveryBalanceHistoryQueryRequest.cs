namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery account-book query request
/// </summary>
public record GateDeliveryBalanceHistoryQueryRequest
{
    public GateFuturesBalanceChangeType? Type { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
}
