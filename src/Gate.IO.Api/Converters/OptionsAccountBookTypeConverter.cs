namespace Gate.IO.Api.Converters;

internal class OptionsAccountBookTypeConverter : BaseConverter<OptionsAccountBookType>
{
    public OptionsAccountBookTypeConverter() : this(true) { }
    public OptionsAccountBookTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsAccountBookType, string>> Mapping => new()
    {
        new KeyValuePair<OptionsAccountBookType, string>(OptionsAccountBookType.DNW, "dnw"),
        new KeyValuePair<OptionsAccountBookType, string>(OptionsAccountBookType.Premium, "prem"),
        new KeyValuePair<OptionsAccountBookType, string>(OptionsAccountBookType.Fee, "fee"),
        new KeyValuePair<OptionsAccountBookType, string>(OptionsAccountBookType.Rebate, "refr"),
        new KeyValuePair<OptionsAccountBookType, string>(OptionsAccountBookType.SettlementPNL, "set"),
    };
}