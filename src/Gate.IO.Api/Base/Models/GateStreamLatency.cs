namespace Gate.IO.Api.Base;

/// <summary>
/// Represents the Gate Stream Latency.
/// </summary>
public record GateStreamLatency
{
    /// <summary>
    /// Gets or sets the Ping Time.
    /// </summary>
    public DateTime PingTime { get; set; }
    /// <summary>
    /// Gets or sets the Pong Time.
    /// </summary>
    public DateTime PongTime { get; set; }
    /// <summary>
    /// Gets or sets the Pong Message.
    /// </summary>
    public string PongMessage { get; set; }
    /// <summary>
    /// Gets or sets the Latency.
    /// </summary>
    public TimeSpan Latency { get; set; }
}
