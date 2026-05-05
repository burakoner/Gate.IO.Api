namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order request
/// </summary>
public record GateFuturesTrailOrderRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Gets or sets the Amount.
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the Activation Price.
    /// </summary>
    [JsonProperty("activation_price", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? ActivationPrice { get; set; }

    /// <summary>
    /// Gets or sets the Is Greater Than Or Equal.
    /// </summary>
    [JsonProperty("is_gte", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsGreaterThanOrEqual { get; set; }

    /// <summary>
    /// Gets or sets the Price Type.
    /// </summary>
    [JsonProperty("price_type", NullValueHandling = NullValueHandling.Ignore)]
    public GateFuturesTrailPriceType? PriceType { get; set; }

    /// <summary>
    /// Gets or sets the Price Offset.
    /// </summary>
    [JsonProperty("price_offset", NullValueHandling = NullValueHandling.Ignore)]
    public string PriceOffset { get; set; }

    /// <summary>
    /// Gets or sets the Reduce Only.
    /// </summary>
    [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Gets or sets the Position Related.
    /// </summary>
    [JsonProperty("position_related", NullValueHandling = NullValueHandling.Ignore)]
    public bool? PositionRelated { get; set; }

    /// <summary>
    /// Gets or sets the Client Order ID.
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Gets or sets the Position Margin Mode.
    /// </summary>
    [JsonProperty("pos_margin_mode", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesPositionMarginMode? PositionMarginMode { get; set; }

    /// <summary>
    /// Gets or sets the Position Mode.
    /// </summary>
    [JsonProperty("position_mode", NullValueHandling = NullValueHandling.Ignore)]
    public string PositionMode { get; set; }
}
