namespace Gate.IO.Api.EarnUni;

/// <summary>
/// EarnUni lending order query request
/// </summary>
public record GateEarnUniLendQueryRequest
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
}
