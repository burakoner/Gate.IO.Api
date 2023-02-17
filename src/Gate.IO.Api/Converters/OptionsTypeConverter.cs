namespace Gate.IO.Api.Converters;

internal class OptionsTypeConverter : BaseConverter<OptionsType>
{
    public OptionsTypeConverter() : this(true) { }
    public OptionsTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsType, string>> Mapping => new()
    {
        new KeyValuePair<OptionsType, string>(OptionsType.Call, "C"),
        new KeyValuePair<OptionsType, string>(OptionsType.Put, "P"),
    };
}