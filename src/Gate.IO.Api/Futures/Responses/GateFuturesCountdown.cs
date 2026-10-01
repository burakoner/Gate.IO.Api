namespace Gate.IO.Api.Futures;

internal record GateFuturesCountdown
{
    /// <summary>
    /// Timestamp of the end of the countdown, in milliseconds
    /// </summary>
    [JsonProperty("triggerTime", Required = Required.Always)]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
