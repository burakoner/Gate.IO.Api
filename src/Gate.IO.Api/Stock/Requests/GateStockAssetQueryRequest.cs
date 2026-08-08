namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock account asset query
/// </summary>
public record GateStockAssetQueryRequest
{
    /// <summary>Gets or sets the profit and loss calculation type.</summary>
    public GateStockPnlCalculationType? PnlCalculationType { get; set; }
    /// <summary>Gets or sets the price source used for profit and loss.</summary>
    public GateStockPnlPriceType? PnlPriceType { get; set; }
}
