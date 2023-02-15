namespace Gate.IO.Api.Converters;

internal class AutoRepaymentStatusConverter : BaseConverter<AutoRepaymentStatus>
{
    public AutoRepaymentStatusConverter() : this(true) { }
    public AutoRepaymentStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<AutoRepaymentStatus, string>> Mapping => new()
    {
        new KeyValuePair<AutoRepaymentStatus, string>(AutoRepaymentStatus.Enabled, "on"),
        new KeyValuePair<AutoRepaymentStatus, string>(AutoRepaymentStatus.Disabled, "off"),
    };
}