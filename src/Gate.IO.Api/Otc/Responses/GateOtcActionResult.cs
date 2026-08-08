namespace Gate.IO.Api.Otc;

/// <summary>
/// OTC action result
/// </summary>
public record GateOtcActionResult
{
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
    /// Response timestamp
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}
