namespace Gate.IO.Api.Options;

/// <summary>
/// Options countdown cancel-all request
/// </summary>
public record GateOptionsCountdownCancelAllRequest
{
    /// <summary>
    /// Gets or sets the Timeout.
    /// </summary>
    public int Timeout { get; set; }
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    public string Underlying { get; set; }
}
