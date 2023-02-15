namespace Gate.IO.Api.Enums;

public enum PriceTriggerCondition
{
    /// <summary>
    /// Enum GreaterThanOrEqualTo for value: >=
    /// </summary>
    [EnumMember(Value = ">=")]
    GreaterThanOrEqualTo = 1,

    /// <summary>
    /// Enum LessThanOrEqualTo for value: <=
    /// </summary>
    [EnumMember(Value = "<=")]
    LessThanOrEqualTo = 2
}
