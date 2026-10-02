namespace Gate.IO.Api.Otc;

/// <summary>Reads documented bank strings and integer values without coercion or timestamp-unit inference.</summary>
public class GateOtcBankValueConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
        => objectType == typeof(string) || objectType == typeof(int) || objectType == typeof(long) || objectType == typeof(long?);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (objectType == typeof(long?) && reader.TokenType == JsonToken.Null) return null;
        if (objectType == typeof(string) && reader.TokenType == JsonToken.String) return (string)reader.Value;
        if (reader.TokenType == JsonToken.Integer)
        {
            var text = Convert.ToString(reader.Value, CultureInfo.InvariantCulture);
            if (objectType == typeof(int) && int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var integer)) return integer;
            if ((objectType == typeof(long) || objectType == typeof(long?))
                && long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value)) return value;
        }
        throw new JsonSerializationException($"Invalid bank value at {reader.Path}");
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null) writer.WriteNull();
        else if (value is long number) writer.WriteValue(number);
        else if (value is int integer) writer.WriteValue(integer);
        else if (value is string text) writer.WriteValue(text);
        else throw new JsonSerializationException("Invalid bank value");
    }
}
