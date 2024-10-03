namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesTriggerCondition
/// </summary>
public enum GateFuturesTriggerCondition
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
