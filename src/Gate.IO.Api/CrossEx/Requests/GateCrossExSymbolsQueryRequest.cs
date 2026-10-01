namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx symbols query request
/// </summary>
public record GateCrossExSymbolsQueryRequest
{
    /// <summary>
    /// Optional trading pair list, one nonblank symbol per element (no embedded CSV).
    /// Null or an empty collection queries all symbols; supplied invalid elements are not silently discarded.
    /// </summary>
    public IEnumerable<string> Symbols { get; set; }
}
