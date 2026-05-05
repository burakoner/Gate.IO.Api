namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction query request
/// </summary>
public record GateTradFiTransactionQueryRequest
{
    public DateTime? BeginTime { get; set; }
    public DateTime? EndTime { get; set; }
    public GateTradFiTransactionType? Type { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}
