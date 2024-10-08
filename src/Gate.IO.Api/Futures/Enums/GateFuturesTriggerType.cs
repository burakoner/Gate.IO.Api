namespace Gate.IO.Api.Futures;

/// <summary>
/// FuturesTriggerOrderType
/// </summary>
public enum GateFuturesTriggerType
{
    /// <summary>
    /// order take-profit/stop-loss, close long position
    /// </summary>
    [Map("close-long-order")]
    CloseLongOrder = 1,

    /// <summary>
    /// order take-profit/stop-loss, close short position
    /// </summary>
    [Map("close-short-order")]
    CloseShortOrder = 2,

    /// <summary>
    /// position take-profit/stop-loss, close long position
    /// </summary>
    [Map("close-long-position")]
    CloseLongPosition = 3,

    /// <summary>
    /// position take-profit/stop-loss, close short position
    /// </summary>
    [Map("close-short-position")]
    CloseShortPosition = 4,

    /// <summary>
    /// position planned take-profit/stop-loss, close long position
    /// </summary>
    [Map("plan-close-long-position")]
    PlanCloseLongPosition = 5,

    /// <summary>
    /// position planned take-profit/stop-loss, close short position
    /// </summary>
    [Map("plan-close-short-position")]
    PlanCloseShortPosition = 6,
}