namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock account fund transfer request
/// </summary>
public record GateStockTransferRequest
{
    /// <summary>Gets or sets the asset. The current API supports USDT only.</summary>
    public string Asset { get; set; } = "USDT";
    /// <summary>Gets or sets the balance change.</summary>
    public decimal Change { get; set; }
    /// <summary>Gets or sets the transfer type.</summary>
    public GateStockTransferType Type { get; set; }
    /// <summary>Gets or sets the idempotent reference identifier.</summary>
    public string ReferenceId { get; set; }
}
