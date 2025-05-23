namespace Gate.IO.Api.Wallet;

/// <summary>
/// Transfer Typpe
/// </summary>
public enum GateWalletTransferType : byte
{
    /// <summary>
    /// Deposit
    /// </summary>
    [Map("deposit")]
    Deposit = 1,

    /// <summary>
    /// Withdrawal
    /// </summary>
    [Map("withdraw")]
    Withdrawal = 2,
}
