namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesOrderAmend
/// </summary>
public record GateFuturesOrderAmend : GateFuturesOrder
{
    /// <summary>
    /// Succeeded
    /// </summary>
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }

    /// <summary>
    /// Error label, only exists if execution fails
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; }

    /// <summary>
    /// Error detail, only present if execution failed and details need to be given
    /// </summary>
    [JsonProperty("detail")]
    public string Detail { get; set; }
}
