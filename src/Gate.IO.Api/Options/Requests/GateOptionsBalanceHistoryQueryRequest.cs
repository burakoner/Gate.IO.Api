namespace Gate.IO.Api.Options;

/// <summary>
/// Options balance history query request
/// </summary>
public record GateOptionsBalanceHistoryQueryRequest
{
    public GateOptionsBalanceChangeType? Type { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
