namespace Gate.IO.Api.Converters;

internal class PriceTriggerConditionConverter : BaseConverter<GateSpotTriggerCondition>
{
    public PriceTriggerConditionConverter() : this(true) { }
    public PriceTriggerConditionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<GateSpotTriggerCondition, string>> Mapping => new()
    {
        new KeyValuePair<GateSpotTriggerCondition, string>(GateSpotTriggerCondition.GreaterThanOrEqualTo, ">="),
        new KeyValuePair<GateSpotTriggerCondition, string>(GateSpotTriggerCondition.LessThanOrEqualTo, "<="),
    };
}