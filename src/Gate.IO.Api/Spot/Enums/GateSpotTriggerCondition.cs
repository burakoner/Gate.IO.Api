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
    GreaterThanOrEqualTo = 1,

    /// <summary>
    /// Enum LessThanOrEqualTo for value: &lt;=
    /// </summary>
    [Map("<=")]
    LessThanOrEqualTo = 2
}
