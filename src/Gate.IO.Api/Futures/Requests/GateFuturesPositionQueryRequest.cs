namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures position-list query request
/// </summary>
public record GateFuturesPositionQueryRequest
{
    public bool? Holding { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
