namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx transfer history query request
/// </summary>
public record GateCrossExTransferHistoryQueryRequest
{
    /// <summary>
    /// Currency
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Order ID or client-defined ID
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum records, max 1000
    /// </summary>
    public int? Limit { get; set; }
}
