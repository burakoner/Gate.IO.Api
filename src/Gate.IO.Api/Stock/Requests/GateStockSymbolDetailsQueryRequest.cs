namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock symbol detail query
/// </summary>
public record GateStockSymbolDetailsQueryRequest
{
    /// <summary>Gets or sets the symbols.</summary>
    public IEnumerable<string> Symbols { get; set; }
    /// <summary>Gets or sets the exchange.</summary>
    public GateStockExchange? Exchange { get; set; }
    /// <summary>Gets or sets the page number.</summary>
    public int? Page { get; set; }
    /// <summary>Gets or sets the page size, up to 500.</summary>
    public int? PageSize { get; set; }
}
