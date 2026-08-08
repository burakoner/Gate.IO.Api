namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures chase order list query request
/// </summary>
public record GateFuturesChaseOrderQueryRequest
{
    /// <summary>
    /// Optional contract name
    /// </summary>
    public string Contract { get; set; }

    /// <summary>
    /// True for finished orders or false for in-progress orders
    /// </summary>
    public bool? IsFinished { get; set; }

    /// <summary>
    /// Lower history time bound. Required with <see cref="EndAt"/> when querying finished orders
    /// </summary>
    public DateTime? StartAt { get; set; }

    /// <summary>
    /// Upper history time bound. Required with <see cref="StartAt"/> when querying finished orders
    /// </summary>
    public DateTime? EndAt { get; set; }

    /// <summary>
    /// Page number starting from 1
    /// </summary>
    public uint? PageNumber { get; set; }

    /// <summary>
    /// Page size between 1 and 100
    /// </summary>
    public uint? PageSize { get; set; }

    /// <summary>
    /// Required sort field
    /// </summary>
    public GateFuturesChaseOrderSort SortBy { get; set; } = GateFuturesChaseOrderSort.CreatedAt;

    /// <summary>
    /// Whether cancelled orders should be hidden
    /// </summary>
    public bool? HideCancelled { get; set; }

    /// <summary>
    /// Optional reduce-only filter
    /// </summary>
    public GateFuturesChaseOrderReduceOnlyFilter? ReduceOnly { get; set; }

    /// <summary>
    /// Optional long or short side filter
    /// </summary>
    public GateFuturesChaseOrderSide? Side { get; set; }
}
