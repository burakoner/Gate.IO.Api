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
    Deposit = 1,

    /// <summary>
    /// Withdraw
    /// </summary>
    [Map("withdraw")]
    Withdraw = 2,

    /// <summary>
    /// Trade Fee Deduct
    /// </summary>
    [Map("trade-fee-deduct")]
    TradeFeeDeduct = 3,

    /// <summary>
    /// Order Create
    /// </summary>
    [Map("order-create")]
    OrderCreate = 4,

    /// <summary>
    /// Order Match
    /// </summary>
    [Map("order-match")]
    OrderMatch = 5,

    /// <summary>
    /// Order Update
    /// </summary>
    [Map("order-update")]
    OrderUpdate = 6,

    /// <summary>
    /// Margin Transfer
    /// </summary>
    [Map("margin-transfer")]
    MarginTransfer = 7,

    /// <summary>
    /// Future Transfer
    /// </summary>
    [Map("future-transfer")]
    FutureTransfer = 8,

    /// <summary>
    /// Cross Margin
    /// </summary>
    [Map("cross-margin")]
    CrossMargin = 9,

    /// <summary>
    /// Other
    /// </summary>
    [Map("other")]
    Other = 10,
}
