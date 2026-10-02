namespace Gate.IO.Api.Otc;

/// <summary>Reads pre-upload strings, Int32 values and Unix seconds without coercion or unit inference.</summary>
public class GateOtcUploadValueConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
        => objectType == typeof(string) || objectType == typeof(int) || objectType == typeof(DateTime);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (objectType == typeof(string) && reader.TokenType == JsonToken.String) return (string)reader.Value;
        if (objectType == typeof(int) && reader.TokenType == JsonToken.Integer
            && int.TryParse(Convert.ToString(reader.Value, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out var integer))
            return integer;
        if (objectType == typeof(DateTime) && reader.TokenType == JsonToken.Integer
            && long.TryParse(Convert.ToString(reader.Value, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out var seconds))
        {
            try { return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime; }
            catch (ArgumentOutOfRangeException) { }
        }
        throw new JsonSerializationException($"Invalid pre-upload value at {reader.Path}");
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is DateTime time)
        {
            // A server timestamp is UTC; saved Unspecified snapshots keep that interpretation.
            if (time.Kind == DateTimeKind.Unspecified) time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
            writer.WriteValue(new DateTimeOffset(time.ToUniversalTime()).ToUnixTimeSeconds());
        }
        else if (value is int integer) writer.WriteValue(integer);
        else if (value is string text) writer.WriteValue(text);
        else throw new JsonSerializationException("Invalid pre-upload value");
    }
}
