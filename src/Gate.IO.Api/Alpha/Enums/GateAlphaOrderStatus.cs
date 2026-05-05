namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha order status.
/// </summary>
public enum GateAlphaOrderStatus
{
    /// <summary>
    /// All statuses.
    /// </summary>
    All = 0,

    /// <summary>
    /// Order is processing.
    /// </summary>
    Processing = 1,

    /// <summary>
    /// Order completed successfully.
    /// </summary>
    Successful = 2,

    /// <summary>
    /// Order failed.
    /// </summary>
    Failed = 3,

    /// <summary>
    /// Order was cancelled.
    /// </summary>
    Cancelled = 4,

    /// <summary>
    /// Buy order was placed but transfer is not completed.
    /// </summary>
    BuyOrderPlacedTransferNotCompleted = 5,

    /// <summary>
    /// Order was cancelled but transfer is not completed.
    /// </summary>
    OrderCancelledTransferNotCompleted = 6,
}
