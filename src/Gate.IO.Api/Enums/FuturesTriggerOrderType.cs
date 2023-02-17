namespace Gate.IO.Api.Enums;

public enum FuturesTriggerOrderType
{
    /// <summary>
    /// order take-profit/stop-loss, close long position
    /// </summary>
    [EnumMember(Value = "close-long-order")]
    CloseLongOrder,

    /// <summary>
    /// order take-profit/stop-loss, close short position
    /// </summary>
    [EnumMember(Value = "close-short-order")]
    CloseShortOrder,

    /// <summary>
    /// position take-profit/stop-loss, close long position
    /// </summary>
    [EnumMember(Value = "close-long-position")]
    CloseLongPosition,

    /// <summary>
    /// position take-profit/stop-loss, close short position
    /// </summary>
    [EnumMember(Value = "close-short-position")]
    CloseShortPosition,

    /// <summary>
    /// position planned take-profit/stop-loss, close long position
    /// </summary>
    [EnumMember(Value = "plan-close-long-position")]
    PlanCloseLongPosition,

    /// <summary>
    /// position planned take-profit/stop-loss, close short position
    /// </summary>
    [EnumMember(Value = "plan-close-short-position")]
    PlanCloseShortPosition,
}