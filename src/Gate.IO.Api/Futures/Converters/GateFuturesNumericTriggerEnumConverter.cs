namespace Gate.IO.Api.Futures;

/// <summary>
/// Converts Futures trigger numeric enums to the integer JSON values used by Gate.IO.
/// </summary>
public class GateFuturesNumericTriggerEnumConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
    {
        var type = Nullable.GetUnderlyingType(objectType) ?? objectType;
        return type == typeof(GateFuturesTriggerStrategy) || type == typeof(GateFuturesTriggerPrice);
    }

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return Nullable.GetUnderlyingType(objectType) == null ? Activator.CreateInstance(objectType) : null;

        var type = Nullable.GetUnderlyingType(objectType) ?? objectType;
        var value = Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture);

        if (type == typeof(GateFuturesTriggerStrategy))
            return (GateFuturesTriggerStrategy)value;

        if (type == typeof(GateFuturesTriggerPrice))
            return (GateFuturesTriggerPrice)value;

        throw new JsonSerializationException($"Unsupported Futures numeric trigger enum type: {type}");
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteValue(Convert.ToInt32(value, CultureInfo.InvariantCulture));
    }
}
