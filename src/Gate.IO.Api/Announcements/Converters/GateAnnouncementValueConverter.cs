namespace Gate.IO.Api.Announcements;

/// <summary>Reads announcement strings and exact integer tokens without coercion, rounding or enum inference.</summary>
public class GateAnnouncementValueConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
        => objectType == typeof(string) || objectType == typeof(int) || objectType == typeof(int?) || objectType == typeof(long);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if ((objectType == typeof(string) || objectType == typeof(int?)) && reader.TokenType == JsonToken.Null) return null;
        if (objectType == typeof(string) && reader.TokenType == JsonToken.String) return (string)reader.Value;
        if (reader.TokenType == JsonToken.Integer)
        {
            var text = Convert.ToString(reader.Value, CultureInfo.InvariantCulture);
            if ((objectType == typeof(int) || objectType == typeof(int?))
                && int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var integer)) return integer;
            if (objectType == typeof(long) && long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var identity)) return identity;
        }
        throw new JsonSerializationException($"Invalid announcement value at {reader.Path}");
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null) writer.WriteNull();
        else if (value is string text) writer.WriteValue(text);
        else if (value is int integer) writer.WriteValue(integer);
        else if (value is long identity) writer.WriteValue(identity);
        else throw new JsonSerializationException("Invalid announcement value");
    }
}
