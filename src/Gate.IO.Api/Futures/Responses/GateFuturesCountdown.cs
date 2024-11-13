namespace Gate.IO.Api.Futures;

internal class GateFuturesCountdown
{
    /// <summary>
    /// Timestamp of the end of the countdown, in milliseconds
    /// </summary>
    [JsonProperty("triggerTime")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
