namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures order query request
/// </summary>
public record GateFuturesOrderQueryRequest
{
    public string Contract { get; set; }
    public GateFuturesOrderStatus Status { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public long? LastId { get; set; }
}
