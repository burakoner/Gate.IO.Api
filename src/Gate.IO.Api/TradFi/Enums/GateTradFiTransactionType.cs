namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction type
/// </summary>
public enum GateTradFiTransactionType
{
    [Map("deposit")]
    Deposit = 1,

    [Map("withdraw")]
    Withdraw = 2,

    [Map("dividend")]
    Dividend = 3,

    [Map("fill_negative")]
    FillNegative = 4,
}
