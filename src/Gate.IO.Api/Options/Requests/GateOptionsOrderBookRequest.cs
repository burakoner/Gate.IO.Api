namespace Gate.IO.Api.Options;

/// <summary>
/// Options order book request
/// </summary>
public record GateOptionsOrderBookRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Interval.
    /// </summary>
    public decimal? Interval { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// Gets or sets the With ID.
    /// </summary>
    public bool? WithId { get; set; }
}
