namespace Gate.IO.Api.Enums;

public enum SpotBalanceChangeType
{
    [EnumMember(Value = "deposit")]
    Deposit,

    [EnumMember(Value = "withdraw")]
    Withdraw,

    [EnumMember(Value = "trade-fee-deduct")]
    TradeFeeDeduct,

    [EnumMember(Value = "order-create")]
    OrderCreate,

    [EnumMember(Value = "order-match")]
    OrderMatch,

    [EnumMember(Value = "order-update")]
    OrderUpdate,

    [EnumMember(Value = "margin-transfer")]
    MarginTransfer,

    [EnumMember(Value = "future-transfer")]
    FutureTransfer,

    [EnumMember(Value = "cross-margin")]
    CrossMargin,

    [EnumMember(Value = "other")]
    Other,
}
