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
}
