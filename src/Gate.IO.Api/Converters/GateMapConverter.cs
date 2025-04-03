namespace Gate.IO.Api.Converters;

/// <summary>
/// Gate.IO Map Converter
/// </summary>
public class GateMapConverter : MapConverter
{
    /// <summary>
    /// Constructor
    /// </summary>
    public GateMapConverter() : base(true, false)
    {
        // Empty
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public GateMapConverter(bool traceOnMissingEntry) : base(true, traceOnMissingEntry)
    {
        // Empty
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public GateMapConverter(bool writeAsInt, bool traceOnMissingEntry) : base(writeAsInt, traceOnMissingEntry)
    {
        // Empty
    }
}
