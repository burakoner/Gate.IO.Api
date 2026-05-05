namespace Gate.IO.Api.P2p;

/// <summary>
/// Ad status update request
/// </summary>
public record GateP2pAdStatusUpdateRequest
{
    /// <summary>
    /// Advertisement ID
    /// </summary>
    public long AdvertisementId { get; set; }

    /// <summary>
    /// New ad status
    /// </summary>
    public GateP2pAdStatusUpdate Status { get; set; }
}
