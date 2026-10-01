namespace Gate.IO.Api.Futures;

/// <summary>
/// Preserves known price-order enum mappings without discarding an explicit unknown value as null.
/// Used only on the shared Futures/Delivery price-order models; other modules keep their converters.
/// </summary>
public class GateFuturesPriceOrderMapConverter : MapConverter
{
    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var explicitValue = reader.TokenType != JsonToken.Null;
        var value = base.ReadJson(reader, objectType, existingValue, serializer);
        if (explicitValue && value == null)
            throw new JsonSerializationException($"Unknown explicit price-order enum at {reader.Path}");
        return value;
    }
}
