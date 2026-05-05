namespace Gate.IO.Api.P2p;

/// <summary>
/// Send chat message request
/// </summary>
public record GateP2pSendChatMessageRequest
{
    /// <summary>
    /// Order ID
    /// </summary>
    public long TransactionId { get; set; }

    /// <summary>
    /// Message type
    /// </summary>
    public GateP2pChatMessageType? Type { get; set; }

    /// <summary>
    /// Message body
    /// </summary>
    public string Message { get; set; }
}
