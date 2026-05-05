namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order change-log query request
/// </summary>
public record GateFuturesTrailOrderChangeLogQueryRequest
{
    public long OrderId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
