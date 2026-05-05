namespace Gate.IO.Api.Alpha;

/// <summary>
/// Request to query Alpha account asset transaction history.
/// </summary>
public record GateAlphaAccountBookRequest
{
    /// <summary>
    /// Start time for the account book query.
    /// </summary>
    public DateTime From { get; set; }

    /// <summary>
    /// End time for the account book query.
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Page number.
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of items returned.
    /// </summary>
    public int? Limit { get; set; }
}
