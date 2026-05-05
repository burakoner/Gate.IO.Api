namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures public liquidation-history query request
/// </summary>
public record GateFuturesLiquidationQueryRequest
{
    public string Contract { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? Limit { get; set; }
}
