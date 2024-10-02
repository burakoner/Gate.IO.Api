namespace Gate.IO.Api.Enums;

public enum FuturesTriggerOrderType
{
    /// <summary>
    /// order take-profit/stop-loss, close long position
    /// </summary>
    [Map("close-long-order")]
    CloseLongOrder,

    /// <summary>
    /// order take-profit/stop-loss, close short position
    /// </summary>
    [Map("close-short-order")]
    CloseShortOrder,

    /// <summary>
    /// position take-profit/stop-loss, close long position
    /// </summary>
    [Map("close-long-position")]
    CloseLongPosition,

    /// <summary>
    /// position take-profit/stop-loss, close short position
    /// </summary>
    [Map("close-short-position")]
    CloseShortPosition,

    /// <summary>
    /// position planned take-profit/stop-loss, close long position
    /// </summary>
    [Map("plan-close-long-position")]
    PlanCloseLongPosition,

    /// <summary>
    /// position planned take-profit/stop-loss, close short position
    /// </summary>
    [Map("plan-close-short-position")]
    PlanCloseShortPosition,
}