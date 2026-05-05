namespace Gate.IO.Api.Options;

/// <summary>
/// Options user liquidation query request
/// </summary>
public record GateOptionsUserLiquidationQueryRequest
{
    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    public string Underlying { get; set; }
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
}
