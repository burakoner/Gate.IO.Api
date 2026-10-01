namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi order submission acknowledgement, not an executed order
/// </summary>
public record GateTradFiOrderId
{
    /// <summary>
    /// Queue task ID, not an order ID. Do not pass it to update or cancel order methods.
    /// The documented string is parsed as an exact Int64 for wrapper compatibility;
    /// nonnumeric or out-of-range values fail deserialization. An omitted ID defaults to zero,
    /// which is not an identified task. HTTP success alone does not confirm order creation or execution.
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }
}
