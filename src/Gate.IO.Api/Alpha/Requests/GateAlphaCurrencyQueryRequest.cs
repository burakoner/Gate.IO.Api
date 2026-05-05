namespace Gate.IO.Api.Alpha;

/// <summary>
/// Request to query Alpha currency information.
/// </summary>
public record GateAlphaCurrencyQueryRequest
{
    /// <summary>
    /// Currency symbol.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page number.
    /// </summary>
    public int? Page { get; set; }
}
