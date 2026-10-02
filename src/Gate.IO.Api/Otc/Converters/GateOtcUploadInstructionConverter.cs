namespace Gate.IO.Api.Otc;

/// <summary>Preserves exact saved upload instructions instead of replacing unknown values with defaults.</summary>
public class GateOtcUploadInstructionConverter : MapConverter
{
    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null && Nullable.GetUnderlyingType(objectType) != null) return null;
        if (reader.TokenType != JsonToken.String)
            throw new JsonSerializationException($"Expected an upload string instruction at {reader.Path}");
        var value = base.ReadJson(reader, objectType, existingValue, serializer);
        var wire = value switch
        {
            GateOtcUploadContentType contentType => MapConverter.GetString(contentType),
            GateOtcUploadScene scene => MapConverter.GetString(scene),
            _ => null,
        };
        if (wire == null || !string.Equals(wire, (string)reader.Value, StringComparison.Ordinal))
            throw new JsonSerializationException($"Unknown upload instruction at {reader.Path}");
        return value;
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value != null && !Enum.IsDefined(value.GetType(), value))
            throw new JsonSerializationException("Undefined upload instruction");
        base.WriteJson(writer, value, serializer);
    }
}
