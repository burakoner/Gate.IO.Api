namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock account fund transfer type
/// </summary>
public enum GateStockTransferType
{
    /// <summary>Deposit into the stock account</summary>
    [Map("deposit")]
    Deposit,
    /// <summary>Withdraw from the stock account</summary>
    [Map("withdraw")]
    Withdraw,
}
