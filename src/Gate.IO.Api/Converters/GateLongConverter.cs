namespace Gate.IO.Api.Converters;

/// <summary>
/// Converts Gate integer fields that can be returned as integer, decimal, or numeric string values.
/// </summary>
public class GateLongConverter : JsonConverter
{
    /// <summary>
    /// Determines whether this converter can convert the specified type.
    /// </summary>
    public override bool CanConvert(Type objectType)
        => objectType == typeof(long) || objectType == typeof(long?);

    /// <summary>
    /// Reads a long or nullable long value.
    /// </summary>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return objectType == typeof(long?) ? null : 0L;

        if (reader.TokenType == JsonToken.String)
        {
            var value = (string)reader.Value;
            if (string.IsNullOrWhiteSpace(value))
                return objectType == typeof(long?) ? null : 0L;

            return ToLong(decimal.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture));
        }

        if (reader.TokenType == JsonToken.Integer)
            return Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture);

        if (reader.TokenType == JsonToken.Float)
            return ToLong(Convert.ToDecimal(reader.Value, CultureInfo.InvariantCulture));

        throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing long.");
    }

    /// <summary>
    /// Writes a long value.
    /// </summary>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        => writer.WriteValue(value);

    private static long ToLong(decimal value)
    {
        var truncated = decimal.Truncate(value);
        if (truncated > long.MaxValue || truncated < long.MinValue)
            throw new JsonSerializationException($"Value {value.ToString(CultureInfo.InvariantCulture)} is outside the range of Int64.");

        return (long)truncated;
    }
}
