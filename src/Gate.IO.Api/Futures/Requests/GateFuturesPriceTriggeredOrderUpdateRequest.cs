namespace Gate.IO.Api.Futures;

/// <summary>
/// Request to modify a Futures price-triggered order.
/// </summary>
public record GateFuturesPriceTriggeredOrderUpdateRequest
{
    /// <summary>
    /// ID of the pending price-triggered order.
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Modified contract quantity. Set to zero for a full close.
    /// </summary>
    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    public long? Size { get; set; }

    /// <summary>
    /// Decimal contract quantity. Takes precedence over <see cref="Size"/> when both are specified.
    /// </summary>
    [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
    public string Amount { get; set; }

    /// <summary>
    /// Modified order price. Set to zero for a market order.
    /// </summary>
    [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
    public string Price { get; set; }

    /// <summary>
    /// Modified trigger price.
    /// </summary>
    [JsonProperty("trigger_price", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerPrice { get; set; }

    /// <summary>
    /// Reference price type.
    /// </summary>
    [JsonProperty("price_type", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(GateFuturesNumericTriggerEnumConverter))]
    public GateFuturesTriggerPrice? PriceType { get; set; }

    /// <summary>
    /// Side to close when fully closing a hedge-mode position.
    /// </summary>
    [JsonProperty("auto_size", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesOrderAutoSize? AutoSize { get; set; }

    /// <summary>
    /// Whether to close the full position in single-position mode.
    /// </summary>
    [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Close { get; set; }
}
