namespace Gate.IO.Api.Converters;

internal class OptionsOrderStatusConverter : BaseConverter<OptionsOrderStatus>
{
    public OptionsOrderStatusConverter() : this(true) { }
    public OptionsOrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsOrderStatus, string>> Mapping => new()
    {
        new KeyValuePair<OptionsOrderStatus, string>(OptionsOrderStatus.Open, "open"),
        new KeyValuePair<OptionsOrderStatus, string>(OptionsOrderStatus.Finished, "finished"),
    };
}