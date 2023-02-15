namespace Gate.IO.Api.Converters;

internal class FuturesStatsIntervalConverter : BaseConverter<FuturesStatsInterval>
{
    public FuturesStatsIntervalConverter() : this(true) { }
    public FuturesStatsIntervalConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesStatsInterval, string>> Mapping => new()
    {
        new KeyValuePair<FuturesStatsInterval, string>(FuturesStatsInterval.FiveMinutes, "5m"),
        new KeyValuePair<FuturesStatsInterval, string>(FuturesStatsInterval.FifteenMinutes, "15m"),
        new KeyValuePair<FuturesStatsInterval, string>(FuturesStatsInterval.ThirtyMinutes, "30m"),
        new KeyValuePair<FuturesStatsInterval, string>(FuturesStatsInterval.OneHour, "1h"),
        new KeyValuePair<FuturesStatsInterval, string>(FuturesStatsInterval.FourHours, "4h"),
        new KeyValuePair<FuturesStatsInterval, string>(FuturesStatsInterval.OneDay, "1d"),
    };
}