namespace Gate.IO.Api.CrossEx;

/// <summary>
/// Maps documented CrossEx string flags to nullable booleans, also accepting legacy boolean tokens.
/// Unrecognized values are rejected rather than interpreted as false.
/// </summary>
public class GateCrossExBooleanStringConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType) => objectType == typeof(bool) || objectType == typeof(bool?);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null && objectType == typeof(bool?)) return null;
        if (reader.TokenType == JsonToken.Boolean) return (bool)reader.Value;
        if (reader.TokenType == JsonToken.String)
        {
            if ((string)reader.Value == "true") return true;
            if ((string)reader.Value == "false") return false;
        }
        throw new JsonSerializationException($"Expected a CrossEx true/false string at {reader.Path}");
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null) writer.WriteNull();
        else writer.WriteValue((bool)value ? "true" : "false");
    }
}
