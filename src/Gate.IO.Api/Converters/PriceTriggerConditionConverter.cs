namespace Gate.IO.Api.Converters;

internal class PriceTriggerConditionConverter : BaseConverter<PriceTriggerCondition>
{
    public PriceTriggerConditionConverter() : this(true) { }
    public PriceTriggerConditionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<PriceTriggerCondition, string>> Mapping => new()
    {
        new KeyValuePair<PriceTriggerCondition, string>(PriceTriggerCondition.GreaterThanOrEqualTo, ">="),
        new KeyValuePair<PriceTriggerCondition, string>(PriceTriggerCondition.LessThanOrEqualTo, "<="),
    };
}