namespace Gate.IO.Api.Futures;

internal class FuturesCountdown
{
    /// <summary>
    /// Timestamp of the end of the countdown, in milliseconds
    /// </summary>
    [JsonProperty("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
