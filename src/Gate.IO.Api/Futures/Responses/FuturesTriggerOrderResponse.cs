namespace Gate.IO.Api.Futures;

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
    [JsonProperty("create_time")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Finished time
    /// </summary>
    [JsonProperty("finish_time")]
    public DateTime? FinishTime { get; set; }

    [JsonProperty("trade_id")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public FuturesPriceTriggerStatus Status { get; set; }

    [JsonProperty("finish_as")]
    public GateFuturesOrderFinishAs? FinishedAs { get; set; }

    /// <summary>
    /// Additional remarks on how the order was finished
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get; set; }
}
