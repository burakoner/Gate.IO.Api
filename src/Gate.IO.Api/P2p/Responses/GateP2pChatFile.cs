namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P chat upload result
/// </summary>
public record GateP2pChatFile
{
    /// <summary>
    /// File key
    /// </summary>
    [JsonProperty("file_key")]
    public string FileKey { get; set; }
}
