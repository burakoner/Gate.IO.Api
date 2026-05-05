namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order request
/// </summary>
public record GateFuturesTrailOrderRequest
{
    [JsonProperty("contract")]
    public string Contract { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("activation_price", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? ActivationPrice { get; set; }

    [JsonProperty("is_gte", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsGreaterThanOrEqual { get; set; }

    [JsonProperty("price_type", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesTrailPriceType? PriceType { get; set; }

    [JsonProperty("price_offset", NullValueHandling = NullValueHandling.Ignore)]
    public string PriceOffset { get; set; }

    [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    [JsonProperty("position_related", NullValueHandling = NullValueHandling.Ignore)]
    public bool? PositionRelated { get; set; }

    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }

    [JsonProperty("pos_margin_mode", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesPositionMarginMode? PositionMarginMode { get; set; }

    [JsonProperty("position_mode", NullValueHandling = NullValueHandling.Ignore)]
    public string PositionMode { get; set; }
}
