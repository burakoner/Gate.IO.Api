namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures ADL history query request
/// </summary>
public record GateFuturesAdlHistoryQueryRequest
{
    public string Contract { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public DateTime? At { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
