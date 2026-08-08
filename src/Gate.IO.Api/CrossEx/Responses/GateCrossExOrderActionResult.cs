namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order action acknowledgement. A successful response means that CrossEx accepted the asynchronous request,
/// not that the venue accepted or executed the order. Confirm the order state through the order query or private order stream.
/// </summary>
public record GateCrossExOrderActionResult
{
    /// <summary>
    /// Order ID used to query the subsequent order state. Its presence does not prove venue acceptance or execution.
    /// </summary>
    [JsonProperty("order_id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Gets or sets the Text.
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }
}
