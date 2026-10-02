namespace Gate.IO.Api.Otc;

/// <summary>Preserves bank string values before the default saved-JSON reader can normalize them into dates.</summary>
public class GateOtcBankJsonConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
        => objectType == typeof(GateOtcBankCreateRequest) || objectType == typeof(GateOtcBankCreateResponse);

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Expected a bank JSON object");
        var dateHandling = reader.DateParseHandling;
        JObject json;
        try
        {
            reader.DateParseHandling = DateParseHandling.None;
            json = JObject.Load(reader);
        }
        finally { reader.DateParseHandling = dateHandling; }
        object result = objectType == typeof(GateOtcBankCreateRequest) ? new GateOtcBankCreateRequest() : new GateOtcBankCreateResponse();
        using var tokenReader = json.CreateReader();
        serializer.Populate(tokenReader, result);
        return result;
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
}
