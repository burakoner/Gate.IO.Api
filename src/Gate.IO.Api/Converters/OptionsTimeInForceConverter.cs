namespace Gate.IO.Api.Converters;

internal class OptionsTimeInForceConverter : BaseConverter<OptionsTimeInForce>
{
    public OptionsTimeInForceConverter() : this(true) { }
    public OptionsTimeInForceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OptionsTimeInForce, string>> Mapping => new()
    {
        new KeyValuePair<OptionsTimeInForce, string>(OptionsTimeInForce.GoodTillCancelled, "gtc"),
        new KeyValuePair<OptionsTimeInForce, string>(OptionsTimeInForce.ImmediateOrCancel, "ioc"),
        new KeyValuePair<OptionsTimeInForce, string>(OptionsTimeInForce.PendingOrCancelled, "poc"),
    };
}