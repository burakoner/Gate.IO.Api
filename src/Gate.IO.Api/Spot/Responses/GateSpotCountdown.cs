namespace Gate.IO.Api.Spot;

/// <summary>
/// SpotCountdown
/// </summary>
internal class GateSpotCountdown
{
    /// <summary>
    /// Timestamp of the end of the countdown, in milliseconds
    /// </summary>
    [JsonProperty("triggerTime")]
    public DateTime Time { get; set; }
}
