namespace Gate.IO.Api.P2p;

/// <summary>
/// Transaction ID request
/// </summary>
public record GateP2pTransactionIdRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }
}
