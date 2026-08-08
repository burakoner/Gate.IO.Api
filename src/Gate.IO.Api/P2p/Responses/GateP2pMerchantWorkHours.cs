namespace Gate.IO.Api.P2p;

/// <summary>
/// Current P2P merchant work status
/// </summary>
public record GateP2pMerchantWorkHours
{
    /// <summary>
    /// Current work status
    /// </summary>
    [JsonProperty("work_status")]
    public GateP2pMerchantWorkStatus WorkStatus { get; set; }
}
