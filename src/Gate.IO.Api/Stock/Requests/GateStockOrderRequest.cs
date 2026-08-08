namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock order request
/// </summary>
public record GateStockOrderRequest
{
    /// <summary>Gets or sets the order volume.</summary>
    public decimal Volume { get; set; }
    /// <summary>Gets or sets the symbol.</summary>
    public string Symbol { get; set; }
    /// <summary>Gets or sets the order side.</summary>
    public GateStockOrderSide Side { get; set; }
    /// <summary>Gets or sets the price type.</summary>
    public GateStockOrderPriceType PriceType { get; set; }
    /// <summary>Gets or sets the trading session.</summary>
    public GateStockTradingSession TradingSession { get; set; }
    /// <summary>Gets or sets the time in force. The current API supports day orders only.</summary>
    public GateStockTimeInForce TimeInForce { get; set; } = GateStockTimeInForce.Day;
    /// <summary>Gets or sets the limit price.</summary>
    public decimal? Price { get; set; }
    /// <summary>Gets or sets the client order identifier.</summary>
    public string ClientOrderId { get; set; }
}
