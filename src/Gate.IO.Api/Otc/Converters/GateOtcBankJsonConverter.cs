namespace Gate.IO.Api.Otc;

/// <summary>Preserves OTC submission strings before the default saved-JSON reader can normalize them into dates.</summary>
public class GateOtcBankJsonConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
        => objectType == typeof(GateOtcBankCreateRequest) || objectType == typeof(GateOtcBankCreateResponse)
        || objectType == typeof(GateOtcBankPersonalSupplementRequest) || objectType == typeof(GateOtcBankEnterpriseSupplementRequest)
        || objectType == typeof(GateOtcMarkOrderPaidRequest);

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException("Expected an OTC submission JSON object");
        var dateHandling = reader.DateParseHandling;
        JObject json;
        try
        {
            reader.DateParseHandling = DateParseHandling.None;
            json = JObject.Load(reader);
        }
        finally { reader.DateParseHandling = dateHandling; }
        object result = Activator.CreateInstance(objectType);
        using var tokenReader = json.CreateReader();
        serializer.Populate(tokenReader, result);
        return result;
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
}
