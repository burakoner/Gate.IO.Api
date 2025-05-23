namespace Gate.IO.Api.Wallet;

/// <summary>
/// Transfer Status
/// </summary>
public enum GateWalletTransferState : byte
{
    /// <summary>
    /// Fail
    /// </summary>
    [Map("FAIL")]
    Fail = 0,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("PENDING")]
    Pending = 1,

    /// <summary>
    /// PartialSuccess
    /// </summary>
    [Map("PARTIAL_SUCCESS")]
    PartialSuccess = 2,

    /// <summary>
    /// Success
    /// </summary>
    [Map("SUCCESS")]
    Success = 3,
}
