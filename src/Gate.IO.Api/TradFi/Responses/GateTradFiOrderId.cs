namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order creation result
/// </summary>
public record GateTradFiOrderId
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }
}
