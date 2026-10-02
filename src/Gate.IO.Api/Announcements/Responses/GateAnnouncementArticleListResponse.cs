namespace Gate.IO.Api.Announcements;

/// <summary>Complete announcement envelope. The schema does not specify a successful business-code value.</summary>
[JsonConverter(typeof(GateAnnouncementJsonConverter))]
public record GateAnnouncementArticleListResponse
{
    /// <summary>Uninterpreted business status code, not the HTTP status.</summary>
    [JsonProperty("code", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public int Code { get; set; }

    /// <summary>Required article page.</summary>
    [JsonProperty("data", Required = Required.Always)]
    public GateAnnouncementArticleListData Data { get; set; }

    /// <summary>Server response message.</summary>
    [JsonProperty("message", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Message { get; set; }

    /// <summary>Server response version.</summary>
    [JsonProperty("version", Required = Required.Always)]
    [JsonConverter(typeof(GateAnnouncementValueConverter))]
    public string Version { get; set; }
}
