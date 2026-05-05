namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha order placement result.
/// </summary>
public record GateAlphaOrderPlacement
{
    /// <summary>
    /// Order ID.
    /// </summary>
    [JsonProperty("order_id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Order status.
    /// </summary>
    [JsonProperty("status")]
    public GateAlphaOrderStatus Status { get; set; }

    /// <summary>
    /// Buy or sell side.
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(MapConverter))]
    public GateAlphaOrderSide Side { get; set; }

    /// <summary>
    /// Trading mode as returned by Gate.
    /// </summary>
    [JsonProperty("gas_mode")]
    public string GasMode { get; set; }

    /// <summary>
    /// Creation timestamp.
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Trade quantity.
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Token contract address.
    /// </summary>
    [JsonProperty("token_address")]
    public string TokenAddress { get; set; }

    /// <summary>
    /// Blockchain name.
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }
}
