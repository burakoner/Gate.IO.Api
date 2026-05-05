namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures personal trade query request
/// </summary>
public record GateFuturesUserTradeQueryRequest
{
    public string Contract { get; set; }
    public long? OrderId { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public long? LastId { get; set; }
}
