namespace Gate.IO.Api.Announcements;

/// <summary>JSON announcement filters. Null means omitted; no server defaults are injected.</summary>
[JsonConverter(typeof(GateAnnouncementJsonConverter))]
public record GateAnnouncementArticleListRequest
{
    /// <summary>Optional article title filter.</summary>
    [JsonProperty("title_query", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string TitleQuery { get; set; }

    /// <summary>Optional page number, sent as a string rather than a numeric JSON token.</summary>
    [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Page { get; set; }

    /// <summary>Optional page size, sent as a string. No endpoint-specific maximum is documented.</summary>
    [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Size { get; set; }

    /// <summary>Optional article tags.</summary>
    [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Tags { get; set; }

    /// <summary>Last N days, with N passed as a string; not an epoch time or a locally calculated date range.</summary>
    [JsonProperty("timer", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Timer { get; set; }

    /// <summary>Optional category name.</summary>
    [JsonProperty("cate_name", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string CategoryName { get; set; }

    /// <summary>Optional category level: "1" or "2".</summary>
    [JsonProperty("cate_level", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string CategoryLevel { get; set; }

    /// <summary>Subsite string: "0" for main, "177" for Turkey; server default "0" when omitted.</summary>
    [JsonProperty("sub_website_id", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string SubWebsiteId { get; set; }

    /// <summary>1 includes pinned articles, 0 excludes them; server default 1 when omitted.</summary>
    [JsonProperty("pinned", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public int? Pinned { get; set; }

    /// <summary>Raw integer update timestamp. No unit conversion is performed.</summary>
    [JsonProperty("update_after", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public int? UpdateAfter { get; set; }

    /// <summary>Unrestricted language string, for example "cn"; independent of stream subscription enums.</summary>
    [JsonProperty("lang", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Language { get; set; }

    /// <summary>1 excludes empty current-language content, 0 keeps it; server default 1 when omitted.</summary>
    [JsonProperty("filter_empty_content", NullValueHandling = NullValueHandling.Ignore)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public int? FilterEmptyContent { get; set; }
}
