namespace Gate.IO.Api.Spot;

/// <summary>
/// GateSpotBalanceChangeType
/// </summary>
public enum GateSpotBalanceChangeType
{
    /// <summary>
    /// Deposit
    /// </summary>
    [Map("deposit")]
    Deposit,

    /// <summary>
    /// Withdraw
    /// </summary>
    [Map("withdraw")]
    Withdraw,

    /// <summary>
    /// Trade Fee Deduct
    /// </summary>
    [Map("trade-fee-deduct")]
    TradeFeeDeduct,

    /// <summary>
    /// Order Create
    /// </summary>
    [Map("order-create")]
    OrderCreate,

    /// <summary>
    /// Order Match
    /// </summary>
    [Map("order-match")]
    OrderMatch,

    /// <summary>
    /// Order Update
    /// </summary>
    [Map("order-update")]
    OrderUpdate,

    /// <summary>
    /// Margin Transfer
    /// </summary>
    [Map("margin-transfer")]
    MarginTransfer,

    /// <summary>
    /// Future Transfer
    /// </summary>
    [Map("future-transfer")]
    FutureTransfer,

    /// <summary>
    /// Cross Margin
    /// </summary>
    [Map("cross-margin")]
    CrossMargin,

    /// <summary>
    /// Other
    /// </summary>
    [Map("other")]
    Other,
}
