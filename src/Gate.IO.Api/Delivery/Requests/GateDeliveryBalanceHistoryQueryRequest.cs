namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery account-book query request
/// </summary>
public record GateDeliveryBalanceHistoryQueryRequest
{
    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    public GateFuturesBalanceChangeType? Type { get; set; }
    /// <summary>
    /// Gets or sets the From.
    /// </summary>
    public DateTime? From { get; set; }
    /// <summary>
    /// Gets or sets the To.
    /// </summary>
    public DateTime? To { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
}
