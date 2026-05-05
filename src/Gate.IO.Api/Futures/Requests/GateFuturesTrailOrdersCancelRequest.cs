namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures trail orders cancel request
/// </summary>
public record GateFuturesTrailOrdersCancelRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Related Position.
    /// </summary>
    public int? RelatedPosition { get; set; }
}
