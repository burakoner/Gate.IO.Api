namespace Gate.IO.Api.Converters;

public class TickerTimezoneConverter : BaseConverter<TickerTimezone>
{
    public TickerTimezoneConverter() : this(true) { }
    public TickerTimezoneConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<TickerTimezone, string>> Mapping => new()
    {
        new KeyValuePair<TickerTimezone, string>(TickerTimezone.UTC0, "utc0"),
        new KeyValuePair<TickerTimezone, string>(TickerTimezone.UTC8, "utc8"),
        new KeyValuePair<TickerTimezone, string>(TickerTimezone.All, "all"),
    };
}
