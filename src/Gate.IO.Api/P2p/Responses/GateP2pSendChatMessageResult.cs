namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P chat message send result
/// </summary>
public record GateP2pSendChatMessageResult
{
    /// <summary>
    /// Server timestamp in milliseconds
    /// </summary>
    [JsonProperty("SRVTM")]
    public long ServerTime { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("txid")]
    public long TransactionId { get; set; }

    /// <summary>
    /// Conversation ID
    /// </summary>
    [JsonProperty("conversation_id")]
    public string ConversationId { get; set; }

    /// <summary>
    /// Message type returned when risk control is triggered
    /// </summary>
    [JsonProperty("msg_type")]
    public GateP2pChatMessageType? MessageType { get; set; }

    /// <summary>
    /// Risk-control display type. 1 indicates off-platform traffic diversion risk.
    /// </summary>
    [JsonProperty("risk_type")]
    public int? RiskType { get; set; }

    /// <summary>
    /// Risk-control prompt
    /// </summary>
    [JsonProperty("toast_msg")]
    public string ToastMessage { get; set; }
}
