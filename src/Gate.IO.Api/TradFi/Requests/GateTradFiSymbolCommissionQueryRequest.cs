namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi symbol commission query request
/// </summary>
public record GateTradFiSymbolCommissionQueryRequest
{
    /// <summary>
    /// Trading symbol code list. At least one symbol or category code is required
    /// </summary>
    public IEnumerable<string> Symbols { get; set; }

    /// <summary>
    /// Category code list. When provided with symbols, filters those symbols by category.
    /// At least one symbol or category code is required
    /// </summary>
    public IEnumerable<string> CategoryCodes { get; set; }
}
