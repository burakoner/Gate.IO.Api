namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotTriggerCondition
/// </summary>
public enum GateSpotTriggerCondition
{
    /// <summary>
    /// Enum GreaterThanOrEqualTo for value: >=
    /// </summary>
    [Map(">=")]
    GreaterThanOrEqualTo,

    /// <summary>
    /// Enum LessThanOrEqualTo for value: <=
    /// </summary>
    [Map("<=")]
    LessThanOrEqualTo
}
