namespace Gate.IO.Api.CrossEx;

/// <summary>
/// Converts CrossEx order book entries encoded as two-item arrays.
/// </summary>
public class GateCrossExStreamOrderBookEntryConverter : JsonConverter
{
    /// <summary>
    /// Determines whether this converter can convert the specified type.
    /// </summary>
    public override bool CanConvert(Type objectType)
        => objectType == typeof(GateCrossExStreamOrderBookEntry);

    /// <summary>
    /// Reads a CrossEx order book entry.
    /// </summary>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        if (token.Type != JTokenType.Array || token.Count() < 2)
            throw new JsonSerializationException("CrossEx order book entry must be a two-item array.");

        return new GateCrossExStreamOrderBookEntry
        {
            Price = token[0]!.Value<decimal>(),
            Quantity = token[1]!.Value<decimal>(),
        };
    }

    /// <summary>
    /// Writes a CrossEx order book entry.
    /// </summary>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var entry = (GateCrossExStreamOrderBookEntry)value;
        writer.WriteStartArray();
        writer.WriteValue(entry.Price);
        writer.WriteValue(entry.Quantity);
        writer.WriteEndArray();
    }
}
