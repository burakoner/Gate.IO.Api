namespace Gate.IO.Api.P2p;

/// <summary>
/// Pending transaction list request
/// </summary>
public record GateP2pPendingTransactionsRequest
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
    /// Order tab
    /// </summary>
    public GateP2pOrderTab? OrderTab { get; set; }

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
}
