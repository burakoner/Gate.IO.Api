namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotTime
/// </summary>
internal class GateSpotTime
{
    /// <summary>
    /// Server current time(ms)
    /// </summary>
    [JsonProperty("server_time")]
    public DateTime Time { get; set; }
}