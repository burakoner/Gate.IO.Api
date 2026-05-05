namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx symbols query request
/// </summary>
public record GateCrossExSymbolsQueryRequest
{
    /// <summary>
    /// Trading pair list
    /// </summary>
    public IEnumerable<string> Symbols { get; set; }
}
