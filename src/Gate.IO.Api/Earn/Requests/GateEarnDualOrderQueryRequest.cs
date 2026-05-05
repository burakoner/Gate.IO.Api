namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment order query request
/// </summary>
public record GateEarnDualOrderQueryRequest
{
    /// <summary>
    /// Start settlement time
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End settlement time
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Product type
    /// </summary>
    public GateEarnDualOptionType? Type { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    public GateEarnDualOrderQueryStatus? Status { get; set; }

    /// <summary>
    /// Investment token
    /// </summary>
    public string Coin { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }
}
