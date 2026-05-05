namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P action result
/// </summary>
public record GateP2pActionResult
{
    /// <summary>
    /// Response timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Placeholder method returned by Gate
    /// </summary>
    [JsonProperty("method")]
    public string Method { get; set; }

    /// <summary>
    /// Return code
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// API version
    /// </summary>
    [JsonProperty("version")]
    public string Version { get; set; }
}
