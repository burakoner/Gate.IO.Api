namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx batch order cancellation request item
/// </summary>
public record GateCrossExBatchCancelOrderRequest
{
    /// <summary>
    /// Order ID. Either this field or <see cref="Text"/> is required. Takes precedence when both are provided.
    /// </summary>
    [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
    public string OrderId { get; set; }

    /// <summary>
    /// Custom ID specified when creating the order. Either this field or <see cref="OrderId"/> is required.
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string Text { get; set; }
}
