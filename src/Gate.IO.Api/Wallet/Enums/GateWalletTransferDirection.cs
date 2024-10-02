namespace Gate.IO.Api.Wallet;

/// <summary>
/// Transfer Direction
/// </summary>
public enum GateWalletTransferDirection
{
    /// <summary>
    /// From
    /// </summary>
    [Map("from")]
    From,

    /// <summary>
    /// To
    /// </summary>
    [Map("to")]
    To,
}
