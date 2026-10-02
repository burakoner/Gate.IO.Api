namespace Gate.IO.Api.CrossEx;

/// <summary>
/// Reads saved CrossEx instructions using exact documented enum strings, not CLR names or numeric defaults.
/// Scoped to order, transfer and quote requests; unrelated response enum behavior is unchanged.
/// </summary>
public class GateCrossExInstructionEnumConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
    {
        var type = Nullable.GetUnderlyingType(objectType) ?? objectType;
        return type == typeof(GateCrossExOrderSide) || type == typeof(GateCrossExOrderType)
            || type == typeof(GateCrossExTimeInForce) || type == typeof(GateCrossExExchangeType)
            || type == typeof(GateCrossExTransferAccountType);
    }

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null && Nullable.GetUnderlyingType(objectType) != null) return null;
        var type = Nullable.GetUnderlyingType(objectType) ?? objectType;
        if (reader.TokenType == JsonToken.String)
            foreach (Enum value in Enum.GetValues(type))
                if (GetWireValue(value) == (string)reader.Value) return value;
        throw new JsonSerializationException($"Expected a documented CrossEx enum string at {reader.Path}");
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null) writer.WriteNull();
        else if (value is Enum mapped && Enum.IsDefined(value.GetType(), value)) writer.WriteValue(GetWireValue(mapped));
        else throw new JsonSerializationException("Invalid CrossEx instruction enum");
    }

    // ApiSharp's generic map lookup needs the concrete enum type, not System.Enum.
    private static string GetWireValue(Enum value) => value switch
    {
        GateCrossExOrderSide side => MapConverter.GetString(side),
        GateCrossExOrderType type => MapConverter.GetString(type),
        GateCrossExTimeInForce time => MapConverter.GetString(time),
        GateCrossExExchangeType exchange => MapConverter.GetString(exchange),
        GateCrossExTransferAccountType account => MapConverter.GetString(account),
        _ => throw new JsonSerializationException("Unsupported CrossEx instruction enum"),
    };
}
