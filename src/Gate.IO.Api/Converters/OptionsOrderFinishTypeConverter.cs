namespace Gate.IO.Api.Converters;

internal class OptionsOrderFinishTypeConverter : BaseConverter<OptionsOrderFinishType>
{
    public OptionsOrderFinishTypeConverter() : this(true) { }
    public OptionsOrderFinishTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsOrderFinishType, string>> Mapping => new()
    {
        new KeyValuePair<OptionsOrderFinishType, string>(OptionsOrderFinishType.Filled, "filled"),
        new KeyValuePair<OptionsOrderFinishType, string>(OptionsOrderFinishType.Cancelled, "cancelled"),
        new KeyValuePair<OptionsOrderFinishType, string>(OptionsOrderFinishType.Liquidated, "liquidated"),
        new KeyValuePair<OptionsOrderFinishType, string>(OptionsOrderFinishType.IOC, "ioc"),
        new KeyValuePair<OptionsOrderFinishType, string>(OptionsOrderFinishType.AutoDeleveraged, "auto_deleveraged"),
        new KeyValuePair<OptionsOrderFinishType, string>(OptionsOrderFinishType.ReduceOnly, "reduce_only"),
        new KeyValuePair<OptionsOrderFinishType, string>(OptionsOrderFinishType.PositionClosed, "position_closed"),
        new KeyValuePair<OptionsOrderFinishType, string>(OptionsOrderFinishType.ReduceOut, "reduce_out"),
    };
}