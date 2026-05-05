namespace Gate.IO.Api.Options;

/// <summary>
/// Options position query request
/// </summary>
public record GateOptionsPositionQueryRequest
{
    /// <summary>
    /// Gets or sets the Underlying.
    /// </summary>
    public string Underlying { get; set; }
}
