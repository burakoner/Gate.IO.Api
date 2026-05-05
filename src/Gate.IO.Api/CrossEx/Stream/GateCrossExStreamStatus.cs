namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx WebSocket operation status.
/// </summary>
public record GateCrossExStreamStatus
{
    /// <summary>
    /// Response code.
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; }

    /// <summary>
    /// Response message.
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// Whether the operation succeeded.
    /// </summary>
    [JsonIgnore]
    public bool Success => Code == "100000";
}
