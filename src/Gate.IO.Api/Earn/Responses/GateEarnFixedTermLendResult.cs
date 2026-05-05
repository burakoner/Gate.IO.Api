namespace Gate.IO.Api.Earn;

/// <summary>
/// Fixed-term Earn subscription result
/// </summary>
public record GateEarnFixedTermLendResult
{
    /// <summary>
    /// Subscription order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }
}
