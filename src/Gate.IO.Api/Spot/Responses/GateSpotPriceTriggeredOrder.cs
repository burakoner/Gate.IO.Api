namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotPriceTriggeredOrder
/// </summary>
public record GateSpotPriceTriggeredOrder : GateSpotPriceTriggeredOrderRequest
{
    /// <summary>
    /// Auto order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user")]
    public long UserId { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("ctime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Finished time
    /// </summary>
    [JsonProperty("ftime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FinishTime { get; set; }

    /// <summary>
    /// ID of the newly created order on condition triggered
    /// </summary>
    [JsonProperty("fired_order_id")]
    public long? FiredOrderId { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public GateSpotTriggerStatus Status { get; set; }

    /// <summary>
    /// Additional remarks on how the order was finished
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }
}
