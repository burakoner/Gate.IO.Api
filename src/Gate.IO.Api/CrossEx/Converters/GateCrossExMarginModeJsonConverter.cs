namespace Gate.IO.Api.CrossEx;

/// <summary>Preserves raw strings and strict object/required-field contracts for saved margin-mode JSON only.</summary>
public class GateCrossExMarginModeJsonConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
        => objectType == typeof(GateCrossExMarginModeQueryRequest) || objectType == typeof(GateCrossExMarginModeRequest)
        || objectType == typeof(GateCrossExMarginModeResponse);

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Expected a CrossEx margin-mode JSON object");
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
        if (result is GateCrossExMarginModeResponse response
            && (string.IsNullOrWhiteSpace(response.Symbol) || string.IsNullOrWhiteSpace(response.MarginMode)))
            throw new JsonSerializationException("Margin-mode response requires a nonblank symbol and mode");
        return result;
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
}
