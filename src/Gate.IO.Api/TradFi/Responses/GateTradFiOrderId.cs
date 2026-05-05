namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order creation result
/// </summary>
public record GateTradFiOrderId
{
    [JsonProperty("id")]
    public long Id { get; set; }
}
