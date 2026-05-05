namespace Gate.IO.Api.Options;

/// <summary>
/// Options order request
/// </summary>
public record GateOptionsOrderRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Size.
    /// </summary>
    public long Size { get; set; }
    /// <summary>
    /// Gets or sets the Iceberg.
    /// </summary>
    public long? Iceberg { get; set; }
    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    public decimal? Price { get; set; }
    /// <summary>
    /// Gets or sets the Close.
    /// </summary>
    public bool? Close { get; set; }
    /// <summary>
    /// Gets or sets the Reduce Only.
    /// </summary>
    public bool? ReduceOnly { get; set; }
    /// <summary>
    /// Gets or sets the MMP.
    /// </summary>
    public bool? Mmp { get; set; }
    /// <summary>
    /// Gets or sets the Time In Force.
    /// </summary>
    public GateOptionsTimeInForce? TimeInForce { get; set; }
    /// <summary>
    /// Gets or sets the Client Order ID.
    /// </summary>
    public string ClientOrderId { get; set; }
}
