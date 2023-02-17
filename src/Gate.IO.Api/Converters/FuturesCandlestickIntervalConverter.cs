namespace Gate.IO.Api.Converters;

internal class FuturesCandlestickIntervalConverter : BaseConverter<FuturesCandlestickInterval>
{
    public FuturesCandlestickIntervalConverter() : this(true) { }
    public FuturesCandlestickIntervalConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesCandlestickInterval, string>> Mapping => new()
    {
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.TenSeconds, "10s"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.OneMinute, "1m"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.FiveMinutes, "5m"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.FifteenMinutes, "15m"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.ThirtyMinutes, "30m"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.OneHour, "1h"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.FourHours, "4h"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.EightHours, "8h"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.OneDay, "1d"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.OneWeek, "7d"),
        new KeyValuePair<FuturesCandlestickInterval, string>(FuturesCandlestickInterval.OneMonth, "30d"),
    };
}