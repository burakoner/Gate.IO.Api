namespace Gate.IO.Api.P2p;

/// <summary>
/// Transaction details request
/// </summary>
public record GateP2pTransactionDetailsRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Channel tag
    /// </summary>
    public string Channel { get; set; }
}
