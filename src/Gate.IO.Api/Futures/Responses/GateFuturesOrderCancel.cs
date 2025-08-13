namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesOrderCancel
/// </summary>
public record GateFuturesOrderCancel
{
    /// <summary>
    /// Futures order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Succeeded
    /// </summary>
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }

    /// <summary>
    /// Error description when cancellation fails, empty if successful
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }
}
