namespace Gate.IO.Api.Spot;

/// <summary>
/// Take-profit or stop-loss settings for a spot limit order. On amendment, an empty object cancels that side and null leaves it unchanged.
/// </summary>
public record GateSpotOrderTpsl
{
    /// <summary>
    /// Trigger price.
    /// </summary>
    [JsonProperty("trigger_price", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerPrice { get; set; }

    /// <summary>
    /// Price of the order placed after the trigger is reached.
    /// </summary>
    [JsonProperty("order_price", NullValueHandling = NullValueHandling.Ignore)]
    public string OrderPrice { get; set; }
}
