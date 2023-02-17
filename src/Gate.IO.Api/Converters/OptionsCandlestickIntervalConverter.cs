namespace Gate.IO.Api.Converters;

internal class OptionsCandlestickIntervalConverter : BaseConverter<OptionsCandlestickInterval>
{
    public OptionsCandlestickIntervalConverter() : this(true) { }
    public OptionsCandlestickIntervalConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsCandlestickInterval, string>> Mapping => new()
    {
        new KeyValuePair<OptionsCandlestickInterval, string>(OptionsCandlestickInterval.OneMinute, "1m"),
        new KeyValuePair<OptionsCandlestickInterval, string>(OptionsCandlestickInterval.FiveMinutes, "5m"),
        new KeyValuePair<OptionsCandlestickInterval, string>(OptionsCandlestickInterval.FifteenMinutes, "15m"),
        new KeyValuePair<OptionsCandlestickInterval, string>(OptionsCandlestickInterval.ThirtyMinutes, "30m"),
        new KeyValuePair<OptionsCandlestickInterval, string>(OptionsCandlestickInterval.OneHour, "1h"),
    };
}