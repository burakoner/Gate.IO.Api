namespace Gate.IO.Api.Models.RestApi.Futures;

internal class FuturesCountdown
{
    /// <summary>
    /// Timestamp of the end of the countdown, in milliseconds
    /// </summary>
    [JsonProperty("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
