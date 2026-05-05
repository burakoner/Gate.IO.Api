namespace Gate.IO.Api.P2p;

/// <summary>
/// Cancel order request
/// </summary>
public record GateP2pCancelOrderRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Cancel reason ID
    /// </summary>
    public string ReasonId { get; set; }

    /// <summary>
    /// Extra cancel notes
    /// </summary>
    public string ReasonMemo { get; set; }
}
