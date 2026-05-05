namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures countdown cancel-all request
/// </summary>
public record GateFuturesCountdownCancelAllRequest
{
    /// <summary>
    /// Gets or sets the Timeout.
    /// </summary>
    public int Timeout { get; set; }
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
}
