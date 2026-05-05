namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni interest record query request
/// </summary>
public record GateEarnUniInterestRecordQueryRequest
{
    /// <summary>
    /// Query by specified currency name
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of items returned
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Start time
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime? To { get; set; }
}
