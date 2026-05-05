namespace Gate.IO.Api.Earn;

/// <summary>
/// Dual investment order query status
/// </summary>
public enum GateEarnDualOrderQueryStatus : byte
{
    /// <summary>
    /// Open position
    /// </summary>
    [Map("HOLD")]
    Hold = 1,

    /// <summary>
    /// Historical position
    /// </summary>
    [Map("REPAY")]
    Repay = 2,

    /// <summary>
    /// Position active
    /// </summary>
    [Map("PROCESSING")]
    Processing = 3,

    /// <summary>
    /// Settlement in progress
    /// </summary>
    [Map("SETTLEMENT_PROCESSING")]
    SettlementProcessing = 4,

    /// <summary>
    /// All orders
    /// </summary>
    [Map("ALL")]
    All = 5,
}
