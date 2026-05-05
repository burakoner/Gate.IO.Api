namespace Gate.IO.Api.Futures;

/// <summary>
/// GateFuturesTriggerCondition
/// </summary>
public enum GateFuturesTriggerCondition : byte
{
    /// <summary>
    /// No trigger condition is set.
    /// </summary>
    [Map("0")]
    None = 0,

    /// <summary>
    /// Greater than or equal to trigger condition
    /// </summary>
    [Map(">=")]
    GreaterThanOrEqualTo = 1,

    /// <summary>
    /// Less than or equal to trigger condition
    /// </summary>
    [Map("<=")]
    LessThanOrEqualTo = 2
}
