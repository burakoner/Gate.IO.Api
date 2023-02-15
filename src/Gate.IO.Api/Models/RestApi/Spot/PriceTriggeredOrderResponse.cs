namespace Gate.IO.Api.Models.RestApi.Spot;

public class PriceTriggeredOrderResponse: PriceTriggeredOrderRequest
{
    /// <summary>
    /// Auto order ID
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; private set; }

    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("user")]
    public int UserId { get; private set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonProperty("ctime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; private set; }

    /// <summary>
    /// Finished time
    /// </summary>
    [JsonProperty("ftime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? FinishTime { get; private set; }

    /// <summary>
    /// ID of the newly created order on condition triggered
    /// </summary>
    [JsonProperty("fired_order_id")]
    public long? FiredOrderId { get;  set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status"), JsonConverter(typeof(PriceTriggerStatusConverter))]
    public PriceTriggerStatus Status { get;  set; }

    /// <summary>
    /// Additional remarks on how the order was finished
    /// </summary>
    [JsonProperty("reason")]
    public string Reason { get;  set; }
}
