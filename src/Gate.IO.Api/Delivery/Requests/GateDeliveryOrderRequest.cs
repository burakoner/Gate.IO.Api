namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery futures order request.
/// </summary>
public record GateDeliveryOrderRequest
{
    /// <summary>
    /// Delivery futures contract.
    /// </summary>
    [JsonProperty("contract")]
    public string Contract { get; set; }

    /// <summary>
    /// Order size. A positive value creates a bid and a negative value creates an ask.
    /// </summary>
    [JsonProperty("size")]
    public long Size { get; set; }

    /// <summary>
    /// Display size for an iceberg order. Zero disables iceberg handling.
    /// </summary>
    [JsonProperty("iceberg", NullValueHandling = NullValueHandling.Ignore)]
    public long? Iceberg { get; set; }

    /// <summary>
    /// Order price. Use zero with IOC for a market order.
    /// </summary>
    [JsonProperty("price"), JsonConverter(typeof(GateDecimalStringConverter))]
    public decimal Price { get; set; }

    /// <summary>
    /// Whether to close the position. Size must be zero when enabled.
    /// </summary>
    [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Close { get; set; }

    /// <summary>
    /// Whether the order is reduce-only.
    /// </summary>
    [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Time in force.
    /// </summary>
    [JsonProperty("tif", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesTimeInForce? TimeInForce { get; set; }

    /// <summary>
    /// User-defined order identifier.
    /// </summary>
    [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Side to close in dual-position mode.
    /// </summary>
    [JsonProperty("auto_size", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesOrderAutoSize? AutoSize { get; set; }

    /// <summary>
    /// Self-trade prevention action.
    /// </summary>
    [JsonProperty("stp_act", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(MapConverter))]
    public GateFuturesSelfTradeAction? SelfTradeAction { get; set; }
}
