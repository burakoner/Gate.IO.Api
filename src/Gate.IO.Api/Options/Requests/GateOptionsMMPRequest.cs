namespace Gate.IO.Api.Options;

/// <summary>
/// Options MMP settings request
/// </summary>
public record GateOptionsMMPRequest
{
    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    public string Underlying { get; set; }
    /// <summary>
    /// Gets or sets the Window.
    /// </summary>
    public int Window { get; set; }
    /// <summary>
    /// Gets or sets the Frozen Period.
    /// </summary>
    public int FrozenPeriod { get; set; }
    /// <summary>
    /// Gets or sets the Quantity Limit.
    /// </summary>
    public decimal QuantityLimit { get; set; }
    /// <summary>
    /// Gets or sets the Delta Limit.
    /// </summary>
    public decimal DeltaLimit { get; set; }
}
