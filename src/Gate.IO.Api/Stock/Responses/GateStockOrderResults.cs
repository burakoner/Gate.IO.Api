namespace Gate.IO.Api.Stock;

/// <summary>
/// Created stock order identifier
/// </summary>
public record GateStockOrderId
{
    /// <summary>Gets or sets the order identifier.</summary>
    [JsonProperty("id")]
    public string Id { get; set; }
}

/// <summary>
/// Updated stock order result
/// </summary>
public record GateStockOrderUpdateResult
{
    /// <summary>Gets or sets the order identifier.</summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }
}

/// <summary>
/// Closed stock position result
/// </summary>
public record GateStockPositionCloseResult
{
    /// <summary>Gets or sets the generated order identifier.</summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }
}
