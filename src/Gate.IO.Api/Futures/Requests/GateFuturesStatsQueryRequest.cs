namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures statistics query request
/// </summary>
public record GateFuturesStatsQueryRequest
{
    public string Contract { get; set; }
    public GateFuturesStatsInterval? Interval { get; set; }
    public DateTime? From { get; set; }
    public int? Limit { get; set; }
}
