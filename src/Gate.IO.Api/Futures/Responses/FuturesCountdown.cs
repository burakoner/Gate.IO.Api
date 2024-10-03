namespace Gate.IO.Api.Futures;

internal class FuturesCountdown
{
    /// <summary>
    /// Timestamp of the end of the countdown, in milliseconds
    /// </summary>
    [JsonProperty("triggerTime")]
    public DateTime Time { get; set; }
}
