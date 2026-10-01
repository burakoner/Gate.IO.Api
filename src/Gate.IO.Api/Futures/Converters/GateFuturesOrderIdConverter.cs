namespace Gate.IO.Api.Futures;

/// <summary>
/// Preserves standard-order Int64 identities without floating-point coercion; writes numeric JSON.
/// </summary>
public class GateFuturesOrderIdConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType) => objectType == typeof(long) || objectType == typeof(long?);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null && objectType == typeof(long?)) return null;
        if ((reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.String)
            && long.TryParse(Convert.ToString(reader.Value, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out var id))
            return id;
        throw new JsonSerializationException($"Expected an exact Int64 identity at {reader.Path}");
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null) writer.WriteNull();
        else writer.WriteValue((long)value);
    }
}
