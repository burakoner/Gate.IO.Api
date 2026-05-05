namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail orders cancel request
/// </summary>
public record GateFuturesTrailOrdersCancelRequest
{
    public string Contract { get; set; }
    public int? RelatedPosition { get; set; }
}
