namespace Gate.IO.Api.Options;

/// <summary>
/// Options market trade query request
/// </summary>
public record GateOptionsTradeQueryRequest
{
    public string Contract { get; set; }
    public GateOptionsType? Type { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
