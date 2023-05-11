namespace Gate.IO.Api.Enums;

public enum FuturesTriggerOrderType
{
    /// <summary>
    /// order take-profit/stop-loss, close long position
    /// </summary>
    [Label("close-long-order")]
    CloseLongOrder,

    /// <summary>
    /// order take-profit/stop-loss, close short position
    /// </summary>
    [Label("close-short-order")]
    CloseShortOrder,

    /// <summary>
    /// position take-profit/stop-loss, close long position
    /// </summary>
    [Label("close-long-position")]
    CloseLongPosition,

    /// <summary>
    /// position take-profit/stop-loss, close short position
    /// </summary>
    [Label("close-short-position")]
    CloseShortPosition,

    /// <summary>
    /// position planned take-profit/stop-loss, close long position
    /// </summary>
    [Label("plan-close-long-position")]
    PlanCloseLongPosition,

    /// <summary>
    /// position planned take-profit/stop-loss, close short position
    /// </summary>
    [Label("plan-close-short-position")]
    PlanCloseShortPosition,
}