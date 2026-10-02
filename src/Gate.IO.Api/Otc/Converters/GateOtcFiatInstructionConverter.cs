namespace Gate.IO.Api.Otc;

/// <summary>Reads saved fiat-order instructions without turning unknown values into defaults.</summary>
public class GateOtcFiatInstructionConverter : MapConverter
{
    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null && Nullable.GetUnderlyingType(objectType) != null) return null;
        if (reader.TokenType != JsonToken.String)
            throw new JsonSerializationException($"Expected a fiat-order string instruction at {reader.Path}");
        var value = base.ReadJson(reader, objectType, existingValue, serializer);
        var mapped = value switch
        {
            GateOtcOrderType type => MapConverter.GetString(type),
            GateOtcOrderKind side when side != GateOtcOrderKind.Stable => MapConverter.GetString(side),
            GateOtcReceiveType receiveType => MapConverter.GetString(receiveType),
            _ => null,
        };
        if (mapped == null || !string.Equals(mapped, (string)reader.Value, StringComparison.Ordinal))
            throw new JsonSerializationException($"Unknown fiat-order instruction at {reader.Path}");
        return value;
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value != null && (!Enum.IsDefined(value.GetType(), value) || value is GateOtcOrderKind.Stable))
            throw new JsonSerializationException("Undefined fiat-order instruction");
        base.WriteJson(writer, value, serializer);
    }
}
