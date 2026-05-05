namespace Gate.IO.Api.P2p;

/// <summary>
/// Advertisement ID request
/// </summary>
public record GateP2pAdvertisementIdRequest
{
    /// <summary>
    /// Advertisement ID
    /// </summary>
    public long AdvertisementId { get; set; }
}
