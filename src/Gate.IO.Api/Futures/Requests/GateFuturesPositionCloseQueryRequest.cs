namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures position-close history query request
/// </summary>
public record GateFuturesPositionCloseQueryRequest
{
    public string Contract { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public GateFuturesPositionSide? Side { get; set; }
    public decimal? Pnl { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
