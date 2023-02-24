namespace Gate.IO.Api.Converters;

internal class OptionsContractPeriodConverter : BaseConverter<OptionsContractPeriod>
{
    public OptionsContractPeriodConverter() : this(true) { }
    public OptionsContractPeriodConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsContractPeriod, string>> Mapping => new()
    {
        new KeyValuePair<OptionsContractPeriod, string>(OptionsContractPeriod.OneWeek, "week"),
        new KeyValuePair<OptionsContractPeriod, string>(OptionsContractPeriod.OneWeek, "WEEK"),
        new KeyValuePair<OptionsContractPeriod, string>(OptionsContractPeriod.OneMonth, "month"),
        new KeyValuePair<OptionsContractPeriod, string>(OptionsContractPeriod.OneMonth, "MONTH"),
    };
}