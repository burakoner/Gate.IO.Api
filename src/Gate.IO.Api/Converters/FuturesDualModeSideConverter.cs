namespace Gate.IO.Api.Converters;

internal class FuturesDualModeSideConverter : BaseConverter<FuturesDualModeSide>
{
    public FuturesDualModeSideConverter() : this(true) { }
    public FuturesDualModeSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<FuturesDualModeSide, string>> Mapping => new()
    {
        new KeyValuePair<FuturesDualModeSide, string>(FuturesDualModeSide.DualLong, "dual_long"),
        new KeyValuePair<FuturesDualModeSide, string>(FuturesDualModeSide.DualShort, "dual_short"),
    };
}