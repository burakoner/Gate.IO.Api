namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi symbol details request
/// </summary>
public record GateTradFiSymbolDetailsRequest
{
    /// <summary>
    /// Gets or sets the Symbols.
    /// </summary>
    public IEnumerable<string> Symbols { get; set; }
}
