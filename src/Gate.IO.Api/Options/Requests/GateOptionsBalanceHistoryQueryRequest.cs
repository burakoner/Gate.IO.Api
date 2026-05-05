namespace Gate.IO.Api.Options;

/// <summary>
/// Options balance history query request
/// </summary>
public record GateOptionsBalanceHistoryQueryRequest
{
    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    public GateOptionsBalanceChangeType? Type { get; set; }
    /// <summary>
    /// Gets or sets the From.
    /// </summary>
    public DateTime? From { get; set; }
    /// <summary>
    /// Gets or sets the To.
    /// </summary>
    public DateTime? To { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// Gets or sets the Offset.
    /// </summary>
    public int? Offset { get; set; }
}
