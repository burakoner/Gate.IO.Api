namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures personal trade time-range query request
/// </summary>
public record GateFuturesUserTradeTimeRangeQueryRequest
{
    public string Contract { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public GateFuturesTradeRole? Role { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
