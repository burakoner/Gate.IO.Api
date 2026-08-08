namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock position query
/// </summary>
public record GateStockPositionQueryRequest
{
    /// <summary>Gets or sets the profit and loss calculation type.</summary>
    public GateStockPnlCalculationType? PnlCalculationType { get; set; }
    /// <summary>Gets or sets the price source used for profit and loss.</summary>
    public GateStockPnlPriceType? PnlPriceType { get; set; }
    /// <summary>Gets or sets the symbol.</summary>
    public string Symbol { get; set; }
    /// <summary>Gets or sets the exchange.</summary>
    public GateStockExchange? Exchange { get; set; }
}
