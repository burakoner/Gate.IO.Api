namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot POV order details
/// </summary>
public record GateSpotPovOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("id")]
    public string OrderId { get; set; }

    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Buy or sell side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(MapConverter))]
    public GateSpotOrderSide Side { get; set; }

    /// <summary>
    /// Trade amount
    /// </summary>
    [JsonProperty("amount"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal Amount { get; set; }

    /// <summary>
    /// Target participation rate
    /// </summary>
    [JsonProperty("participation_rate"), JsonConverter(typeof(GateMapConverter))]
    public GateSpotPovParticipationRate ParticipationRate { get; set; }

    /// <summary>
    /// Time to live
    /// </summary>
    [JsonProperty("ttl"), JsonConverter(typeof(MapConverter))]
    public GateSpotPovTimeToLive TimeToLive { get; set; }

    /// <summary>
    /// Limit price. A missing value means that the market price is used.
    /// </summary>
    [JsonProperty("limit_price"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? LimitPrice { get; set; }

    /// <summary>
    /// Trigger price. A missing value means that the order is triggered immediately.
    /// </summary>
    [JsonProperty("trigger_price"), JsonConverter(typeof(GateDecimalConverter))]
    public decimal? TriggerPrice { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(MapConverter))]
    public GateSpotPovOrderStatus Status { get; set; }

    /// <summary>
    /// Order termination reason code
    /// </summary>
    [JsonProperty("terminated_as")]
    public string TerminatedAs { get; set; }

    /// <summary>
    /// Order execution start time in milliseconds
    /// </summary>
    [JsonProperty("start_time_ms")]
    public long? StartTimeInMilliseconds { get; set; }

    /// <summary>
    /// Order execution end time in milliseconds
    /// </summary>
    [JsonProperty("end_time_ms")]
    public long? EndTimeInMilliseconds { get; set; }

    /// <summary>
    /// Order expiration time in milliseconds
    /// </summary>
    [JsonProperty("expire_time_ms")]
    public long? ExpireTimeInMilliseconds { get; set; }

    /// <summary>
    /// Creation time in milliseconds
    /// </summary>
    [JsonProperty("create_time_ms")]
    public long CreateTimeInMilliseconds { get; set; }

    /// <summary>
    /// Last modification time in milliseconds
    /// </summary>
    [JsonProperty("update_time_ms")]
    public long? UpdateTimeInMilliseconds { get; set; }

    /// <summary>
    /// User-defined order ID
    /// </summary>
    [JsonProperty("text")]
    public string ClientOrderId { get; set; }
}
