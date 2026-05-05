namespace Gate.IO.Api.Announcements;

internal record GateAnnouncementStreamRequest
{
    [JsonProperty("time")]
    public long Timestamp { get; set; } = DateTime.UtcNow.ConvertToSeconds();

    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("payload")]
    public object Payload { get; set; }
}
