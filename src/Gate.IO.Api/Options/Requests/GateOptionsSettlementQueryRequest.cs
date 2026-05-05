namespace Gate.IO.Api.Options;

/// <summary>
/// Options settlement query request
/// </summary>
public record GateOptionsSettlementQueryRequest
{
    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    public string Underlying { get; set; }
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
