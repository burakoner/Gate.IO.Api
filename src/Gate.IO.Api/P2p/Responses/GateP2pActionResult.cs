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
    /// Business result code as supplied by Gate. Null means no code was supplied and is not success.
    /// An explicit zero means success; an HTTP success alone does not imply business success.
    /// Advertisement code 70305102 means content risk control rejected the submission; inspect Data for details.
    /// </summary>
    [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
    public int? BusinessCode { get; set; }

    /// <summary>
    /// Compatibility accessor. Missing codes still read as zero; use BusinessCode == 0 to confirm an explicit success code.
    /// </summary>
    [JsonIgnore]
    public int Code
    {
        get => BusinessCode ?? 0;
        set => BusinessCode = value;
    }

    /// <summary>
    /// Message
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Action response data. Advertisement submissions return risk-control details here when rejected.
    /// </summary>
    [JsonProperty("data")]
    public GateP2pActionData Data { get; set; }

    /// <summary>
    /// API version
    /// </summary>
    [JsonProperty("version")]
    public string Version { get; set; }
}
