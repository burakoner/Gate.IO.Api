namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures price-triggered order identifier.
/// </summary>
public record GateFuturesPriceTriggeredOrderId
{
    /// <summary>
    /// Auto order ID.
    /// </summary>
    [JsonProperty("id")]
    public long OrderId { get; set; }

    /// <summary>
    /// String form of the auto order ID. Prefer this value where an Int64-safe string identifier is required.
    /// </summary>
    [JsonProperty("id_string")]
    public string OrderIdString { get; set; }
}
