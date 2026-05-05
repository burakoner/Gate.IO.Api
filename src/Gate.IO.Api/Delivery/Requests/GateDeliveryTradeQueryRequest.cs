namespace Gate.IO.Api.Delivery;

/// <summary>
/// Delivery public trade query request
/// </summary>
public record GateDeliveryTradeQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the From.
    /// </summary>
    public DateTime? From { get; set; }
    /// <summary>
    /// Gets or sets the To.
    /// </summary>
    public DateTime? To { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// Gets or sets the Offset.
    /// </summary>
    public int? Offset { get; set; }
    /// <summary>
    /// Gets or sets the Last ID.
    /// </summary>
    public long? LastId { get; set; }
}
