namespace Gate.IO.Api.Options;

/// <summary>
/// Options user trade query request
/// </summary>
public record GateOptionsUserTradeQueryRequest
{
    public string Underlying { get; set; }
    public string Contract { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
