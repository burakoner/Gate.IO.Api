namespace Gate.IO.Api.Announcements;

/// <summary>Raw language code and display name supported by an article.</summary>
[JsonConverter(typeof(GateAnnouncementJsonConverter))]
public record GateAnnouncementArticleLanguage
{
    /// <summary>Language code, for example "cn".</summary>
    [JsonProperty("code", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Code { get; set; }

    /// <summary>Language display name.</summary>
    [JsonProperty("name", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Name { get; set; }
}
