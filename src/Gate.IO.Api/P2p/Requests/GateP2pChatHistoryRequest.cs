namespace Gate.IO.Api.P2p;

/// <summary>
/// Chat history request
/// </summary>
public record GateP2pChatHistoryRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long? TransactionId { get; set; }

    /// <summary>
    /// Timestamp of the last received message
    /// </summary>
    public DateTime? LastReceived { get; set; }

    /// <summary>
    /// Timestamp of first received message
    /// </summary>
    public DateTime? FirstReceived { get; set; }
}
