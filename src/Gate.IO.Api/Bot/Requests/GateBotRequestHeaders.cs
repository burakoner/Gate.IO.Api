namespace Gate.IO.Api.Bot;

/// <summary>
/// Optional Bot request headers
/// </summary>
public record GateBotRequestHeaders
{
    /// <summary>
    /// Call source identifier
    /// </summary>
    public string ServiceId { get; set; }

    /// <summary>
    /// Language context, for example en-US
    /// </summary>
    public string AppLanguage { get; set; }

    /// <summary>
    /// Request link ID
    /// </summary>
    public string RequestId { get; set; }

    /// <summary>
    /// Trace header
    /// </summary>
    public string TraceId { get; set; }
}
