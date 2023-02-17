namespace Gate.IO.Api.Models.RestApi.Futures;

public class FuturesTriggerOrderResponse : FuturesTriggerOrderRequest
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
    public int UserId { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("create_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Finished time
    /// </summary>
    [JsonProperty("finish_time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FinishTime { get; set; }

    [JsonProperty("trade_id")]
    public long? TradeId { get;  set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(FuturesPriceTriggerStatusConverter))]
    public FuturesPriceTriggerStatus Status { get; set; }

    [JsonProperty("finish_as"), JsonConverter(typeof(FuturesOrderFinishTypeConverter))]
    public FuturesOrderFinishType? FinishedAs { get; set; }

    /// <summary>
    /// Additional remarks on how the order was finished
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get;  set; }
}
