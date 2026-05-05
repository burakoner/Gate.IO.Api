namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet UID transfer history query request
/// </summary>
public record GateWalletTransferHistoryQueryRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    /// Order type returned in the list
    /// </summary>
    public GateWalletTransferType? Type { get; set; }

    /// <summary>
    /// Start time for querying records
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End time for querying records
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Maximum number of items returned in the list
    /// </summary>
    public int? Limit { get; set; } = 100;

    /// <summary>
    /// List offset, starting from 0
    /// </summary>
    public int? Offset { get; set; } = 0;
}
