namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx leverage query request
/// </summary>
public record GateCrossExLeverageQueryRequest
{
    /// <summary>
    /// Trading pair list
    /// </summary>
    public IEnumerable<string> Symbols { get; set; }
}
