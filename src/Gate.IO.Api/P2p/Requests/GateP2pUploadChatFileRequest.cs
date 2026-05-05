namespace Gate.IO.Api.P2p;

/// <summary>
/// Upload chat file request
/// </summary>
public record GateP2pUploadChatFileRequest
{
    /// <summary>
    /// File MIME type
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Base64 file content
    /// </summary>
    public string Base64Content { get; set; }
}
