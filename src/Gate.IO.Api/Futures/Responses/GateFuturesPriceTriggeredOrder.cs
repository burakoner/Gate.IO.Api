namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesPriceTriggeredOrder
/// </summary>
public record GateFuturesPriceTriggeredOrder : GateFuturesPriceTriggeredOrderRequest
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
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Finished time
    /// </summary>
    [JsonProperty("finish_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FinishTime { get; set; }

    /// <summary>
    /// ID of the newly created order on condition triggered
    /// </summary>
    [JsonProperty("trade_id")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public GateFuturesPriceTriggerStatus Status { get; set; }

    /// <summary>
    /// How order is finished
    /// </summary>
    [JsonProperty("finish_as")]
    public GateFuturesOrderFinishAs? FinishAs { get; set; }

    /// <summary>
    /// Additional remarks on how the order was finished
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }

    /// <summary>
    /// Corresponding order ID of order take-profit/stop-loss.
    /// </summary>
    [JsonProperty("me_order_id")]
    public long MeOrderId { get; set; }
}
