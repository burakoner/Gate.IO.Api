namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order cancel request
/// </summary>
public record GateFuturesTrailOrderCancelRequest
{
    public long? OrderId { get; set; }
    public string ClientOrderId { get; set; }
}
