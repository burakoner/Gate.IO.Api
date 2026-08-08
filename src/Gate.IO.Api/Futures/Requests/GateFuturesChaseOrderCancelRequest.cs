namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase order cancellation request
/// </summary>
public record GateFuturesChaseOrderCancelRequest
{
    /// <summary>
    /// Order ID. Either this field or <see cref="ClientOrderId"/> must be provided
    /// </summary>
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string OrderId { get; set; }

    /// <summary>
    /// Custom order tag. Required when the order ID is omitted
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }
}
