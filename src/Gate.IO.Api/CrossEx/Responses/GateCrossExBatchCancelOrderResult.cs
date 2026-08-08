namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx batch order cancellation result
/// </summary>
public record GateCrossExBatchCancelOrderResult
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Custom ID specified when creating the order
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    /// <summary>
    /// Whether the request was accepted, returned by Gate as the string <c>true</c> or <c>false</c>
    /// </summary>
    [JsonProperty("accepted")]
    public string Accepted { get; set; }

    /// <summary>
    /// Error label when the request is not accepted; empty on success
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; }

    /// <summary>
    /// Error message when the request is not accepted; empty on success
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }
}
