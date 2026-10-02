namespace Gate.IO.Api.Announcements;

/// <summary>Announcement article metadata. Time, category, source and status values are not inferred or normalized.</summary>
[JsonConverter(typeof(GateAnnouncementJsonConverter))]
public record GateAnnouncementArticle
{
    /// <summary>Exact article identity.</summary>
    [JsonProperty("id", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public long Id { get; set; }

    /// <summary>Article title.</summary>
    [JsonProperty("title", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Title { get; set; }

    /// <summary>Article summary, retained as text without rendering or following embedded links.</summary>
    [JsonProperty("brief", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Brief { get; set; }

    /// <summary>Raw creation time string.</summary>
    [JsonProperty("created", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Created { get; set; }

    /// <summary>Raw update time string.</summary>
    [JsonProperty("updated", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Updated { get; set; }

    /// <summary>Raw publication time string.</summary>
    [JsonProperty("release_time", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string ReleaseTime { get; set; }

    /// <summary>View count.</summary>
    [JsonProperty("views", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public int Views { get; set; }

    /// <summary>Author text.</summary>
    [JsonProperty("author", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Author { get; set; }

    /// <summary>Exact author identity.</summary>
    [JsonProperty("author_id", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public long AuthorId { get; set; }

    /// <summary>Raw article tags.</summary>
    [JsonProperty("tags", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Tags { get; set; }

    /// <summary>Raw category.</summary>
    [JsonProperty("cate", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Category { get; set; }

    /// <summary>Raw integer pinned indicator, including unfamiliar values.</summary>
    [JsonProperty("is_top", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public int IsTop { get; set; }

    /// <summary>Exact category identity.</summary>
    [JsonProperty("cate_id", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public long CategoryId { get; set; }

    /// <summary>Raw source text.</summary>
    [JsonProperty("source", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Source { get; set; }

    /// <summary>Languages available for this article, not restricted to the stream subscription enum.</summary>
    [JsonProperty("content_lang_list", Required = Required.Always)]
    public List<GateAnnouncementArticleLanguage> ContentLanguages { get; set; }
}
