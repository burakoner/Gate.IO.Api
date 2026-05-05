namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P ad status update result
/// </summary>
public record GateP2pAdStatusResult
{
    /// <summary>
    /// Updated ad status
    /// </summary>
    [JsonProperty("status")]
    public GateP2pAdStatusUpdate? Status { get; set; }
}
