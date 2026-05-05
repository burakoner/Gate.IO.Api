namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order action result
/// </summary>
public record GateCrossExOrderActionResult
{
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}
