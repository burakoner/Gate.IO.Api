namespace Gate.IO.Api.Converters;

/// <summary>
/// Converts Gate numeric fields that can occasionally be sent as an empty string.
/// </summary>
public class GateDecimalConverter : JsonConverter
{
    /// <summary>
    /// Determines whether this converter can convert the specified type.
    /// </summary>
    public override bool CanConvert(Type objectType)
        => objectType == typeof(decimal) || objectType == typeof(decimal?);

    /// <summary>
    /// Reads a decimal or nullable decimal value.
    /// </summary>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return objectType == typeof(decimal?) ? null : 0m;

        if (reader.TokenType == JsonToken.String)
        {
            var value = (string)reader.Value;
            if (IsNullLike(value))
                return objectType == typeof(decimal?) ? null : 0m;

            return decimal.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float)
            return Convert.ToDecimal(reader.Value, CultureInfo.InvariantCulture);

        throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing decimal.");
    }

    /// <summary>
    /// Writes a decimal value.
    /// </summary>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        => writer.WriteValue(value);

    private static bool IsNullLike(string value)
        => string.IsNullOrWhiteSpace(value)
        || string.Equals(value, "∞", StringComparison.Ordinal)
        || string.Equals(value, "+∞", StringComparison.Ordinal)
        || string.Equals(value, "Infinity", StringComparison.OrdinalIgnoreCase)
        || string.Equals(value, "+Infinity", StringComparison.OrdinalIgnoreCase);
}
