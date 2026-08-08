namespace Gate.IO.Api.Converters;

/// <summary>
/// Reads Gate decimal values from strings or numbers and writes them as invariant-culture strings.
/// </summary>
public class GateDecimalStringConverter : GateDecimalConverter
{
    /// <summary>
    /// Writes a decimal value as a JSON string.
    /// </summary>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteValue(Convert.ToDecimal(value, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture));
    }
}
