namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order list query request
/// </summary>
public record GateFuturesTrailOrderQueryRequest
{
    public string Contract { get; set; }
    public bool? IsFinished { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public int? SortBy { get; set; }
    public bool? HideCancel { get; set; }
    public int? RelatedPosition { get; set; }
    public bool? SortByTrigger { get; set; }
    public int? ReduceOnly { get; set; }
    public int? Side { get; set; }
}
