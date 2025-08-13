namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesOrderAmendRequest
/// </summary>
public record GateFuturesOrderAmendRequest
{
    /// <summary>
    /// Order id, order_id and text must contain at least one
    /// </summary>
    [JsonProperty("order_id")]
    public long? OrderId { get; set; }

    /// <summary>
    /// User-defined order text, at least one of order_id and text must be passed
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// New order size, including filled size.
    /// - If less than or equal to the filled quantity, the order will be cancelled.
    /// - The new order side must be identical to the original one.
    /// - Close order size cannot be modified.
    /// - For reduce-only orders, increasing the size may cancel other reduce-only orders.
    /// - If the price is not modified, decreasing the size will not affect the depth queue, while increasing the size will place it at the end of the current price level.
    /// </summary>
    [JsonProperty("size")]
    public decimal? Size { get; set; }

    /// <summary>
    /// New order price
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Custom info during order amendment
    /// </summary>
    [JsonProperty("amend_text")]
    public string AmendText { get; set; }
}
