namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction type
/// </summary>
public enum GateTradFiTransactionType
{
    /// <summary>
    /// Represents the Deposit value.
    /// </summary>
    [Map("deposit")]
    Deposit = 1,

    /// <summary>
    /// Represents the Withdraw value.
    /// </summary>
    [Map("withdraw")]
    Withdraw = 2,

    /// <summary>
    /// Represents the Dividend value.
    /// </summary>
    [Map("dividend")]
    Dividend = 3,

    /// <summary>
    /// Represents the Fill Negative value.
    /// </summary>
    [Map("fill_negative")]
    FillNegative = 4,
}
