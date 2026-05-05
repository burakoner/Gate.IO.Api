namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order action result
/// </summary>
public record GateCrossExOrderActionResult
{
    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Gets or sets the Text.
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }
}
