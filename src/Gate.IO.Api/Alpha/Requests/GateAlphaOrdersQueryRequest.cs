namespace Gate.IO.Api.Alpha;

/// <summary>
/// Request to query Alpha orders.
/// </summary>
public record GateAlphaOrdersQueryRequest
{
    /// <summary>
    /// Trading symbol.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Buy or sell side.
    /// </summary>
    public GateAlphaOrderSide? Side { get; set; }

    /// <summary>
    /// Order status.
    /// </summary>
    public GateAlphaOrderStatus? Status { get; set; }

    /// <summary>
    /// Start time for order query.
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End time for order query.
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Maximum number of items returned. Default 100, minimum 1, maximum 100.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page number.
    /// </summary>
    public int? Page { get; set; }
}
