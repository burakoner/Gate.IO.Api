namespace Gate.IO.Api.Options;

/// <summary>
/// Options cancel orders request
/// </summary>
public record GateOptionsCancelOrdersRequest
{
    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    public string Underlying { get; set; }
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    public GateOptionsOrderSide? Side { get; set; }
}
