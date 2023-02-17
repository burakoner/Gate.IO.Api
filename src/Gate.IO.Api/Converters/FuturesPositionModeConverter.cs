namespace Gate.IO.Api.Converters;

internal class FuturesPositionModeConverter : BaseConverter<FuturesPositionMode>
{
    public FuturesPositionModeConverter() : this(true) { }
    public FuturesPositionModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesPositionMode, string>> Mapping => new()
    {
        new KeyValuePair<FuturesPositionMode, string>(FuturesPositionMode.Single, "single"),
        new KeyValuePair<FuturesPositionMode, string>(FuturesPositionMode.DualLong, "dual_long"),
        new KeyValuePair<FuturesPositionMode, string>(FuturesPositionMode.DualShort, "dual_short"),
    };
}