namespace Gate.IO.Api.Converters;

internal class CandlestickIntervalConverter : BaseConverter<CandlestickInterval>
{
    public CandlestickIntervalConverter() : this(true) { }
    public CandlestickIntervalConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<CandlestickInterval, string>> Mapping => new()
    {
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.TenSeconds, "10s"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.OneMinute, "1m"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.FiveMinutes, "5m"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.FifteenMinutes, "15m"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.ThirtyMinutes, "30m"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.OneHour, "1h"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.FourHours, "4h"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.EightHours, "8h"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.OneDay, "1d"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.OneWeek, "7d"),
        new KeyValuePair<CandlestickInterval, string>(CandlestickInterval.OneMonth, "30d"),
    };
}