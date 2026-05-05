namespace Gate.IO.Api.Options;

/// <summary>
/// Options order update request
/// </summary>
public record GateOptionsOrderUpdateRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Price.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Gets or sets the Size.
    /// </summary>
    public long Size { get; set; }
}
