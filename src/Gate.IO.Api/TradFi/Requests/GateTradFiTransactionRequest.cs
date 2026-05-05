namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction request
/// </summary>
public record GateTradFiTransactionRequest
{
    public string Asset { get; set; }
    public decimal Change { get; set; }
    public GateTradFiTransactionType Type { get; set; }
}
