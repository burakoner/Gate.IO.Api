namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures historical position query request
/// </summary>
public record GateFuturesHistoricalPositionQueryRequest
{
    public string Contract { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
