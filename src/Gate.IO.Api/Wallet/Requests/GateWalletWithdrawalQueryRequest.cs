namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet withdrawal records query request
/// </summary>
public record GateWalletWithdrawalQueryRequest
{
    /// <summary>
    /// Specify the currency. If not specified, returns all currencies
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Withdrawal record ID
    /// </summary>
    public string WithdrawalId { get; set; }

    /// <summary>
    /// User-defined order number for withdrawal
    /// </summary>
    public string WithdrawalOrderId { get; set; }

    /// <summary>
    /// Currency type of withdrawal record
    /// </summary>
    public GateWalletAssetClass? AssetClass { get; set; }

    /// <summary>
    /// Start time for querying records
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp for the query
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; } = 100;

    /// <summary>
    /// List offset, starting from 0
    /// </summary>
    public int? Offset { get; set; } = 0;
}
