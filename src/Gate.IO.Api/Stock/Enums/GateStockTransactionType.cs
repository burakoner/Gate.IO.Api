namespace Gate.IO.Api.Stock;

/// <summary>
/// Stock transaction type
/// </summary>
public enum GateStockTransactionType
{
    /// <summary>Deposit</summary>
    [Map("deposit")]
    Deposit,
    /// <summary>Withdrawal</summary>
    [Map("withdraw")]
    Withdraw,
    /// <summary>Fee</summary>
    [Map("fee")]
    Fee,
    /// <summary>Dividend</summary>
    [Map("dividend")]
    Dividend,
    /// <summary>Sell</summary>
    [Map("sell")]
    Sell,
    /// <summary>Buy</summary>
    [Map("buy")]
    Buy,
    /// <summary>Award</summary>
    [Map("award")]
    Award,
    /// <summary>Stock transfer in</summary>
    [Map("stock_transfer_in")]
    StockTransferIn,
    /// <summary>Stock transfer out</summary>
    [Map("stock_transfer_out")]
    StockTransferOut,
}
