namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotTriggerCondition
/// </summary>
public enum GateSpotTriggerCondition
{
    /// <summary>
    /// Enum GreaterThanOrEqualTo for value: &gt;=
    /// </summary>
    [Map(">=")]
    GreaterThanOrEqualTo,

    /// <summary>
    /// Enum LessThanOrEqualTo for value: &lt;=
    /// </summary>
    [Map("<=")]
    LessThanOrEqualTo
}
