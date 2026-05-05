namespace Gate.IO.Api.Announcements;

/// <summary>
/// Announcement summary stream update.
/// </summary>
public record GateAnnouncementStreamSummary
{
    /// <summary>
    /// Announcement language.
    /// </summary>
    [JsonProperty("lang"), JsonConverter(typeof(MapConverter))]
    public GateAnnouncementLanguage Language { get; set; }

    /// <summary>
    /// Original announcement URL.
    /// </summary>
    [JsonProperty("origin_url")]
    public string OriginUrl { get; set; }

    /// <summary>
    /// Announcement title.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    /// Announcement brief content.
    /// </summary>
    [JsonProperty("brief")]
    public string Brief { get; set; }

    /// <summary>
    /// Announcement publication time.
    /// </summary>
    [JsonProperty("published_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime PublishedTime { get; set; }
}
