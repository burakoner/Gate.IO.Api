namespace Gate.IO.Api.Wallet;

/// <summary>
/// Trading account transfer status
/// </summary>
public enum GateWalletTradingAccountTransferStatus : byte
{
    /// <summary>Processing</summary>
    [Map("pending")]
    Pending = 1,

    /// <summary>Successful</summary>
    [Map("success")]
    Success = 2,

    /// <summary>Failed</summary>
    [Map("fail")]
    Fail = 3,
}
