namespace Gate.IO.Api.Futures;

/// <summary>
/// Converts the shared trigger condition enum to the numeric values expected by Futures trigger orders.
/// </summary>
public class GateFuturesTriggerConditionConverter : JsonConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
        => objectType == typeof(GateSpotTriggerCondition) || objectType == typeof(GateSpotTriggerCondition?);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return objectType == typeof(GateSpotTriggerCondition?) ? null : GateSpotTriggerCondition.GreaterThanOrEqualTo;

        var value = Convert.ToString(reader.Value, CultureInfo.InvariantCulture);
        return value switch
        {
            "1" or ">=" => GateSpotTriggerCondition.GreaterThanOrEqualTo,
            "2" or "<=" => GateSpotTriggerCondition.LessThanOrEqualTo,
            _ => throw new JsonSerializationException($"Unknown Futures trigger condition value: {value}")
        };
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var condition = (GateSpotTriggerCondition)value;
        writer.WriteValue(condition switch
        {
            GateSpotTriggerCondition.GreaterThanOrEqualTo => 1,
            GateSpotTriggerCondition.LessThanOrEqualTo => 2,
            _ => throw new JsonSerializationException($"Unknown Futures trigger condition value: {condition}")
        });
    }
}
