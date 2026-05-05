namespace Gate.IO.Api.Options;

/// <summary>
/// Options contract query request
/// </summary>
public record GateOptionsContractQueryRequest
{
    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    public string Underlying { get; set; }
    /// <summary>
    /// Gets or sets the Expiration.
    /// </summary>
    public long? Expiration { get; set; }
}
