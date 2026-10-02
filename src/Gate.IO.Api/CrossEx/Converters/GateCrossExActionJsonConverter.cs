namespace Gate.IO.Api.CrossEx;

/// <summary>Preserves opaque strings in saved order/transfer acknowledgements and quotes before date parsing.</summary>
public class GateCrossExActionJsonConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
        => objectType == typeof(GateCrossExOrderActionResult) || objectType == typeof(GateCrossExTransferResult)
        || objectType == typeof(GateCrossExConvertQuote);

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Expected a CrossEx action JSON object");
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
        return result;
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
}
