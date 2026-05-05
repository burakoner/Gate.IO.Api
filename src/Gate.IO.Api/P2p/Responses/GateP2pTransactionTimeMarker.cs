namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P transaction time marker
/// </summary>
public record GateP2pTransactionTimeMarker
{
    /// <summary>
    /// Countdown time
    /// </summary>
    [JsonProperty("od_time")]
    public long? OrderTime { get; set; }
}
