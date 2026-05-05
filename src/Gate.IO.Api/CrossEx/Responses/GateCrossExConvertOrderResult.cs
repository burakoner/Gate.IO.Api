namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx flash swap order result
/// </summary>
public record GateCrossExConvertOrderResult
{
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}
