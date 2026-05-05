namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi symbol details request
/// </summary>
public record GateTradFiSymbolDetailsRequest
{
    public IEnumerable<string> Symbols { get; set; }
}
