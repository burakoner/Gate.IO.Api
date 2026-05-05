namespace Gate.IO.Api.CrossEx;

internal record GateCrossExStreamRequest
{
    [JsonProperty("time")]
    public long Timestamp { get; set; } = DateTime.UtcNow.ConvertToSeconds();

    [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
    public string Channel { get; set; }

    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("payload", NullValueHandling = NullValueHandling.Ignore)]
    public object Payload { get; set; }
}
