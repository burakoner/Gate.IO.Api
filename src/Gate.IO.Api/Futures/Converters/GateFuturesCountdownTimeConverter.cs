namespace Gate.IO.Api.Futures;

/// <summary>Countdown triggerTime is exact Unix milliseconds, not an inferred seconds/milliseconds value.</summary>
public class GateFuturesCountdownTimeConverter : GateFuturesOrderIdConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type objectType) => objectType == typeof(DateTime);

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var milliseconds = (long)base.ReadJson(reader, typeof(long), existingValue, serializer);
        try { return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime; }
        catch (ArgumentOutOfRangeException exception) { throw new JsonSerializationException("Countdown timestamp is outside the supported date range", exception); }
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var time = (DateTime)value;
        writer.WriteValue(new DateTimeOffset(time.Kind == DateTimeKind.Local ? time.ToUniversalTime() : DateTime.SpecifyKind(time, DateTimeKind.Utc)).ToUnixTimeMilliseconds());
    }
}
