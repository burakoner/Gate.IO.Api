namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesTriggerCondition
/// </summary>
public enum GateFuturesTriggerCondition : byte
{
    /// <summary>
    /// Enum GreaterThanOrEqualTo for value: >=
    /// </summary>
    [Map(">=")]
    GreaterThanOrEqualTo = 1,

    /// <summary>
    /// Enum LessThanOrEqualTo for value: <=
    /// </summary>
    [Map("<=")]
    LessThanOrEqualTo = 2
}
