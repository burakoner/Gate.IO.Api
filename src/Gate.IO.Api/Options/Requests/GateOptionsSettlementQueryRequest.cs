namespace Gate.IO.Api.Options;

/// <summary>
/// Options settlement query request
/// </summary>
public record GateOptionsSettlementQueryRequest
{
    public string Underlying { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
