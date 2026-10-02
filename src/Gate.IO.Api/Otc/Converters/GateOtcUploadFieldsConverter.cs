namespace Gate.IO.Api.Otc;

/// <summary>Preserves case-sensitive, opaque S3 form strings, including future fields.</summary>
public class GateOtcUploadFieldsConverter : JsonConverter
{
    private static readonly string[] RequiredFields =
        { "key", "Content-Type", "X-Amz-Credential", "X-Amz-Algorithm", "X-Amz-Date", "Policy", "X-Amz-Signature" };

    /// <inheritdoc />
    public override bool CanConvert(Type objectType) => objectType == typeof(Dictionary<string, string>);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonSerializationException("Invalid pre-upload Policy fields");

        // Change the reader before consuming values, so saved JSON cannot turn signed strings into dates.
        var dateHandling = reader.DateParseHandling;
        JObject fields;
        try
        {
            reader.DateParseHandling = DateParseHandling.None;
            fields = JObject.Load(reader);
        }
        finally { reader.DateParseHandling = dateHandling; }

        var result = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var field in fields.Properties())
        {
            if (field.Value.Type != JTokenType.String)
                throw new JsonSerializationException("Invalid pre-upload Policy field value");
            result.Add(field.Name, (string)field.Value);
        }
        if (RequiredFields.Any(field => !result.ContainsKey(field)))
            throw new JsonSerializationException("Incomplete pre-upload Policy fields");
        return result;
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is not Dictionary<string, string> fields)
            throw new JsonSerializationException("Invalid pre-upload Policy fields");
        writer.WriteStartObject();
        foreach (var field in fields)
        {
            writer.WritePropertyName(field.Key);
            writer.WriteValue(field.Value);
        }
        writer.WriteEndObject();
    }
}
