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
    /// Legacy DateTime view, explicitly retained for compatibility. OtcActionResponse does not state a unit.
    /// The shared converter infers units and treats 0/-1 as default; fiat creation retains its separate seconds view.
    /// Saved result JSON follows this legacy converter, not an exact reproduction of the server's integer.
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }
}
