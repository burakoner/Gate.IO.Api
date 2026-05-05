namespace Gate.IO.Api.CrossEx;

/// <summary>
/// Represents a CrossEx WebSocket response.
/// </summary>
public record GateCrossExStreamResponse<T>
{
    /// <summary>
    /// Response timestamp.
    /// </summary>
    [JsonProperty("time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Response timestamp in milliseconds.
    /// </summary>
    [JsonProperty("time_ms")]
    public long TimeInMilliseconds { get; set; }

    /// <summary>
    /// Channel name.
    /// </summary>
    [JsonProperty("channel")]
    public string Channel { get; set; }

    /// <summary>
    /// Event name.
    /// </summary>
    [JsonProperty("event")]
    public string Event { get; set; }

    /// <summary>
    /// Request payload or pushed private data.
    /// </summary>
    [JsonProperty("payload")]
    public T Payload { get; set; }

    /// <summary>
    /// Response status.
    /// </summary>
    [JsonProperty("result")]
    public GateCrossExStreamStatus Result { get; set; }
}
