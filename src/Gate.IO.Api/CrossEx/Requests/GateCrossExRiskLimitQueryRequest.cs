namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx risk limit query request
/// </summary>
public record GateCrossExRiskLimitQueryRequest
{
    /// <summary>
    /// Trading pair list
    /// </summary>
    public IEnumerable<string> Symbols { get; set; }
}
