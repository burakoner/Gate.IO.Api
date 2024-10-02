namespace Gate.IO.Api.Spot;

public enum GateSpotBalanceChangeType
{
    [Map("deposit")]
    Deposit,

    [Map("withdraw")]
    Withdraw,

    [Map("trade-fee-deduct")]
    TradeFeeDeduct,

    [Map("order-create")]
    OrderCreate,

    [Map("order-match")]
    OrderMatch,

    [Map("order-update")]
    OrderUpdate,

    [Map("margin-transfer")]
    MarginTransfer,

    [Map("future-transfer")]
    FutureTransfer,

    [Map("cross-margin")]
    CrossMargin,

    [Map("other")]
    Other,
}
