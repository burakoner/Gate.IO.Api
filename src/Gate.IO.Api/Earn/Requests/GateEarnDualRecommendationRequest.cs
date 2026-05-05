namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment recommendation request
/// </summary>
public record GateEarnDualRecommendationRequest
{
    /// <summary>
    /// Sort mode
    /// </summary>
    public GateEarnDualRecommendationMode? Mode { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    public GateEarnDualOptionType? Type { get; set; }

    /// <summary>
    /// Project IDs to exclude
    /// </summary>
    public IEnumerable<long> HistoryProductIds { get; set; }
}
