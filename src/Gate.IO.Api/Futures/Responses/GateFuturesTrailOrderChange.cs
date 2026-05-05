namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order change-log item
/// </summary>
public record GateFuturesTrailOrderChange
{
    [JsonProperty("updated_at")]
    public long UpdatedAt { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("is_gte")]
    public bool IsGreaterThanOrEqual { get; set; }

    [JsonProperty("activation_price")]
    public decimal ActivationPrice { get; set; }

    [JsonProperty("price_type")]
    public GateFuturesTrailPriceType PriceType { get; set; }

    [JsonProperty("price_offset")]
    public string PriceOffset { get; set; }

    [JsonProperty("is_create")]
    public bool IsCreate { get; set; }
}
