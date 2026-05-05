namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures order query request
/// </summary>
public record GateFuturesOrderQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    public GateFuturesOrderStatus Status { get; set; }
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
