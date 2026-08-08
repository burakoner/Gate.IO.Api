namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase order request
/// </summary>
public record GateFuturesChaseOrderRequest
{
    /// <summary>
    /// Contract name
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Total order size in contracts as a decimal string. Positive for buy and negative for sell
    /// </summary>
    [JsonProperty("amount")]
    public string Amount { get; set; }

    /// <summary>
    /// Maximum chase price as a decimal string. Use 0 when no price limit is set
    /// </summary>
    [JsonProperty("price_limit")]
    public string PriceLimit { get; set; }

    /// <summary>
    /// Maximum distance from the best price. Mutually exclusive with a non-zero price limit
    /// </summary>
    [JsonProperty("offset_limit", NullValueHandling = NullValueHandling.Ignore)]
    public string OffsetLimit { get; set; }

    /// <summary>
    /// Whether the order is reduce-only
    /// </summary>
    [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Custom order tag
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Whether dual-position mode is enabled
    /// </summary>
    [JsonProperty("is_dual_mode", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsDualMode { get; set; }

    /// <summary>
    /// Chase price type
    /// </summary>
    [JsonProperty("price_type", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesChaseOrderPriceType? PriceType { get; set; }

    /// <summary>
    /// Price gap type used when the price type is <see cref="GateFuturesChaseOrderPriceType.PriceGap"/>
    /// </summary>
    [JsonProperty("price_gap_type", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesChaseOrderPriceGapType? PriceGapType { get; set; }

    /// <summary>
    /// Price gap value paired with the price gap type
    /// </summary>
    [JsonProperty("price_gap_value", NullValueHandling = NullValueHandling.Ignore)]
    public string PriceGapValue { get; set; }

    /// <summary>
    /// Position margin mode
    /// </summary>
    [JsonProperty("pos_margin_mode", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesPositionMarginMode? PositionMarginMode { get; set; }

    /// <summary>
    /// Position mode, for example single, dual, or dual_plus
    /// </summary>
    [JsonProperty("position_mode", NullValueHandling = NullValueHandling.Ignore)]
    public string PositionMode { get; set; }
}
