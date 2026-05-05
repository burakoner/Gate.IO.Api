namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures funding-rate history query request
/// </summary>
public record GateFuturesFundingRateQueryRequest
{
    public string Contract { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
}
