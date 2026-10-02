namespace Gate.IO.Api.Otc;

/// <summary>Stops malformed OTC JSON from reaching the dependency's payload-bearing parser errors.</summary>
internal sealed class GateOtcSensitiveEnvelopeConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(JToken);
    public override bool CanWrite => false;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        try { return JToken.ReadFrom(reader); }
        catch (JsonException) { return null; }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        => throw new NotSupportedException();
}
