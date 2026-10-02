namespace Gate.IO.Api.CrossEx;

/// <summary>
/// Reads action/quote strings without token coercion. Keeps valid_ms as an exact Int64 while writing its documented string token.
/// </summary>
public class GateCrossExActionValueConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType) => objectType == typeof(string) || objectType == typeof(long);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (objectType == typeof(string))
        {
            if (reader.TokenType == JsonToken.Null) return null;
            if (reader.TokenType == JsonToken.String) return (string)reader.Value;
        }
        else if ((reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer)
            && long.TryParse(Convert.ToString(reader.Value, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
            return value;
        throw new JsonSerializationException($"Expected an exact CrossEx action value at {reader.Path}");
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null) writer.WriteNull();
        else if (value is string text) writer.WriteValue(text);
        else if (value is long number) writer.WriteValue(number.ToString(CultureInfo.InvariantCulture));
        else throw new JsonSerializationException("Invalid CrossEx action value");
    }
}
