namespace Gate.IO.Api.P2p;

/// <summary>
/// Completed transaction list request
/// </summary>
public record GateP2pCompletedTransactionsRequest
{
    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    public string CryptoCurrency { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string FiatCurrency { get; set; }

    /// <summary>
    /// Order side filter
    /// </summary>
    public GateP2pOrderSide? SelectType { get; set; }

    /// <summary>
    /// Order status filter
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    public long? TransactionId { get; set; }

    /// <summary>
    /// Start timestamp
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Whether to flag dispute status in the response
    /// </summary>
    public bool? QueryDispute { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Orders per page
    /// </summary>
    public int? PerPage { get; set; }
}
