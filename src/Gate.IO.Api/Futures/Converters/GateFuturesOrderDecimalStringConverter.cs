namespace Gate.IO.Api.Futures;

/// <summary>
/// Reads saved order instructions without silently rounding or replacing explicit values with zero/null.
/// Writes invariant decimal strings. Floating JSON tokens are rejected because readers may already have rounded them.
/// </summary>
public class GateFuturesOrderDecimalStringConverter : GateDecimalStringConverter
{
    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null && Nullable.GetUnderlyingType(objectType) != null) return null;
        string text = null;
        if (reader.TokenType == JsonToken.String) text = (string)reader.Value;
        else if (reader.TokenType == JsonToken.Integer)
            text = Convert.ToString(reader.Value, CultureInfo.InvariantCulture);
        if (text != null && decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)
            && Normalize(text) is string exact && exact == Normalize(value.ToString(CultureInfo.InvariantCulture)))
            return value;
        throw new JsonSerializationException($"Expected an exact decimal string at {reader.Path}; no rounding or zero substitution is allowed");
    }

    // Compare significand/exponent without allocating powers of ten, including tiny underflowing inputs.
    private static string Normalize(string text)
    {
        var match = Regex.Match(text, @"\A(?<sign>[+-]?)(?<whole>[0-9]*)(?:\.(?<fraction>[0-9]*))?(?:[eE](?<exponent>[+-]?[0-9]+))?\z");
        if (!match.Success) return null;
        var digits = (match.Groups["whole"].Value + match.Groups["fraction"].Value).TrimStart('0');
        if (digits.Length == 0) return "0";
        long exponent = 0;
        if (match.Groups["exponent"].Success)
        {
            if (!int.TryParse(match.Groups["exponent"].Value, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var parsed)) return null;
            exponent = parsed;
        }
        var significant = digits.TrimEnd('0');
        exponent += digits.Length - significant.Length - (long)match.Groups["fraction"].Length;
        return (match.Groups["sign"].Value == "-" ? "-" : "") + significant + "e" + exponent.ToString(CultureInfo.InvariantCulture);
    }
}
