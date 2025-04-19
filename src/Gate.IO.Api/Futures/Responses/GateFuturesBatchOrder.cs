namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesBatchOrder
/// </summary>
public record GateFuturesBatchOrder : GateFuturesOrder
{
    /// <summary>
    /// Whether the batch of orders succeeded
    /// </summary>
    [JsonProperty("succeeded")]
    public bool Succeeded { get; set; }

    /// <summary>
    /// Error label, only exists if execution fails
    /// </summary>
    [JsonProperty("label")]
    public string ErrorLabel { get; set; }

    /// <summary>
    /// Error detail, only present if execution failed and details need to be given
    /// </summary>
    [JsonProperty("detail")]
    public string ErrorDetail { get; set; }
}