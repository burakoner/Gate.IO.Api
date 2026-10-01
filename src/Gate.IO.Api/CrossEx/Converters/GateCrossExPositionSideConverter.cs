namespace Gate.IO.Api.CrossEx;

/// <summary>
/// Reads explicit isolated-margin position instructions without discarding unknown values into the one-way default.
/// </summary>
public class GateCrossExPositionSideConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType) => objectType == typeof(GateCrossExPositionSide) || objectType == typeof(GateCrossExPositionSide?);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null && objectType == typeof(GateCrossExPositionSide?)) return null;
        if (reader.TokenType == JsonToken.String)
        {
            switch ((string)reader.Value)
            {
                case "NONE": return GateCrossExPositionSide.None;
                case "LONG": return GateCrossExPositionSide.Long;
                case "SHORT": return GateCrossExPositionSide.Short;
            }
        }
        throw new JsonSerializationException($"Expected NONE, LONG or SHORT at {reader.Path}");
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null) { writer.WriteNull(); return; }
        if (!Enum.IsDefined(typeof(GateCrossExPositionSide), value))
            throw new JsonSerializationException("Undefined CrossEx position side");
        writer.WriteValue(MapConverter.GetString((GateCrossExPositionSide)value));
    }
}
