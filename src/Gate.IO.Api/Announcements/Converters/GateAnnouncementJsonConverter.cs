namespace Gate.IO.Api.Announcements;

/// <summary>Preserves announcement strings in saved JSON before the default reader can convert them into dates.</summary>
public class GateAnnouncementJsonConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
        => objectType == typeof(GateAnnouncementArticleListRequest) || objectType == typeof(GateAnnouncementArticleListResponse)
        || objectType == typeof(GateAnnouncementArticleListData) || objectType == typeof(GateAnnouncementArticle)
        || objectType == typeof(GateAnnouncementArticleLanguage);

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Expected an announcement JSON object");
        var dateHandling = reader.DateParseHandling;
        JObject json;
        try
        {
            reader.DateParseHandling = DateParseHandling.None;
            json = JObject.Load(reader);
        }
        finally { reader.DateParseHandling = dateHandling; }
        var result = Activator.CreateInstance(objectType);
        using var tokenReader = json.CreateReader();
        serializer.Populate(tokenReader, result);
        if (result is GateAnnouncementArticleListData page && page.List.Any(x => x == null)
            || result is GateAnnouncementArticle article && article.ContentLanguages.Any(x => x == null))
            throw new JsonSerializationException("Announcement arrays require object entries");
        return result;
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
}
