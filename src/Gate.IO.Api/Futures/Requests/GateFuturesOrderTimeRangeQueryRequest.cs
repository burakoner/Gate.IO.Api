namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures order time-range query request
/// </summary>
public record GateFuturesOrderTimeRangeQueryRequest
{
    public string Contract { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
