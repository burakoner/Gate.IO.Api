namespace Gate.IO.Api.Options;

/// <summary>
/// Gate Options Countdown
/// </summary>
internal record GateOptionsCountdown
{
    /// <summary>
    /// Timestamp of the end of the countdown, in milliseconds
    /// </summary>
    [JsonProperty("triggerTime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
