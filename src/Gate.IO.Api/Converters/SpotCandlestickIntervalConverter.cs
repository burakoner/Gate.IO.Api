namespace Gate.IO.Api.Converters;

internal class SpotCandlestickIntervalConverter : BaseConverter<SpotCandlestickInterval>
{
    public SpotCandlestickIntervalConverter() : this(true) { }
    public SpotCandlestickIntervalConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotCandlestickInterval, string>> Mapping => new()
    {
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.TenSeconds, "10s"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.OneMinute, "1m"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.FiveMinutes, "5m"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.FifteenMinutes, "15m"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.ThirtyMinutes, "30m"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.OneHour, "1h"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.FourHours, "4h"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.EightHours, "8h"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.OneDay, "1d"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.OneWeek, "7d"),
        new KeyValuePair<SpotCandlestickInterval, string>(SpotCandlestickInterval.OneMonth, "30d"),
    };
}