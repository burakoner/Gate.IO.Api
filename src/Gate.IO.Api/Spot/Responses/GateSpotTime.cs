namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotTime
/// </summary>
internal record GateSpotTime
{
    /// <summary>
    /// Server current time(ms)
    /// </summary>
    [JsonProperty("server_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}