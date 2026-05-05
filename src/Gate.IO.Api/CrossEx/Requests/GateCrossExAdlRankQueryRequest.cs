namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx ADL rank query request
/// </summary>
public record GateCrossExAdlRankQueryRequest
{
    /// <summary>
    /// Trading pair
    /// </summary>
    public string Symbol { get; set; }
}
