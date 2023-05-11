namespace Gate.IO.Api.Enums;

public enum SpotBalanceChangeType
{
    [Label("deposit")]
    Deposit,

    [Label("withdraw")]
    Withdraw,

    [Label("trade-fee-deduct")]
    TradeFeeDeduct,

    [Label("order-create")]
    OrderCreate,

    [Label("order-match")]
    OrderMatch,

    [Label("order-update")]
    OrderUpdate,

    [Label("margin-transfer")]
    MarginTransfer,

    [Label("future-transfer")]
    FutureTransfer,

    [Label("cross-margin")]
    CrossMargin,

    [Label("other")]
    Other,
}
