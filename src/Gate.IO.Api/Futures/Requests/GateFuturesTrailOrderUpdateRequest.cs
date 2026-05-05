namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail order update request
/// </summary>
public record GateFuturesTrailOrderUpdateRequest
{
    public long OrderId { get; set; }
    public decimal? Amount { get; set; }
    public decimal? ActivationPrice { get; set; }
    public bool? IsGreaterThanOrEqual { get; set; }
    public GateFuturesTrailPriceType? PriceType { get; set; }
    public string PriceOffset { get; set; }
}
