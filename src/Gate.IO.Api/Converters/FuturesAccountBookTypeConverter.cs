namespace Gate.IO.Api.Converters;

internal class FuturesAccountBookTypeConverter : BaseConverter<FuturesAccountBookType>
{
    public FuturesAccountBookTypeConverter() : this(true) { }
    public FuturesAccountBookTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesAccountBookType, string>> Mapping => new()
    {
        new KeyValuePair<FuturesAccountBookType, string>(FuturesAccountBookType.DNW, "dnw"),
        new KeyValuePair<FuturesAccountBookType, string>(FuturesAccountBookType.PNL, "pnl"),
        new KeyValuePair<FuturesAccountBookType, string>(FuturesAccountBookType.Fee, "fee"),
        new KeyValuePair<FuturesAccountBookType, string>(FuturesAccountBookType.Rebate, "refr"),
        new KeyValuePair<FuturesAccountBookType, string>(FuturesAccountBookType.Funding, "fund"),
        new KeyValuePair<FuturesAccountBookType, string>(FuturesAccountBookType.PointDNW, "point_dnw"),
        new KeyValuePair<FuturesAccountBookType, string>(FuturesAccountBookType.PointFee, "point_fee"),
        new KeyValuePair<FuturesAccountBookType, string>(FuturesAccountBookType.PointRebate, "point_refr"),
    };
}