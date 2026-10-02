namespace Gate.IO.Api.Announcements;

/// <summary>Article list and total count; an empty list is valid but a missing list is not.</summary>
[JsonConverter(typeof(GateAnnouncementJsonConverter))]
public record GateAnnouncementArticleListData
{
    /// <summary>Required article list.</summary>
    [JsonProperty("list", Required = Required.Always)]
    public List<GateAnnouncementArticle> List { get; set; }

    /// <summary>Total article count, distinct from the current page length.</summary>
    [JsonProperty("total", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public int Total { get; set; }
}
