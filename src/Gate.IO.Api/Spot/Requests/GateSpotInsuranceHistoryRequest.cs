namespace Gate.IO.Api.Spot;

/// <summary>
/// Spot insurance history query request
/// </summary>
public record GateSpotInsuranceHistoryRequest
{
    /// <summary>
    /// Leverage business
    /// </summary>
    public string Business { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Start timestamp
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End timestamp
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }
}
