namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures personal trade query request
/// </summary>
public record GateFuturesUserTradeQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Order ID.
    /// </summary>
    public long? OrderId { get; set; }
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
