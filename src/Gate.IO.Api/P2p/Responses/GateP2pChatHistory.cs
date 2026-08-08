namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P chat history
/// </summary>
public record GateP2pChatHistory
{
    /// <summary>
    /// Messages
    /// </summary>
    [JsonProperty("messages")]
    public List<GateP2pChatMessage> Messages { get; set; } = [];

    /// <summary>
    /// Payment tip
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; }

    /// <summary>
    /// Whether older chat records exist
    /// </summary>
    [JsonProperty("has_history")]
    public bool HasHistory { get; set; }

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("txid")]
    public long? TransactionId { get; set; }

    /// <summary>
    /// Timestamp of the latest message
    /// </summary>
    [JsonProperty("SRVTM")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ServerTime { get; set; }

    /// <summary>
    /// Raw order status
    /// </summary>
    [JsonProperty("order_status")]
    public string OrderStatus { get; set; }
}
