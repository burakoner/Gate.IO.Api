namespace Gate.IO.Api.Wallet;

/// <summary>
/// UID Transfer Status
/// </summary>
public enum GateWalletUidTransferState : byte
{
    /// <summary>
    /// Creating
    /// </summary>
    [Map("CREATING")]
    Creating = 0,

    /// <summary>
    /// Pending
    /// </summary>
    [Map("PENDING")]
    Pending = 1,

    /// <summary>
    /// Cancelling
    /// </summary>
    [Map("CANCELLING")]
    Cancelling = 2,

    /// <summary>
    /// Cancelled
    /// </summary>
    [Map("CANCELLED")]
    Cancelled = 3,

    /// <summary>
    /// Refusing
    /// </summary>
    [Map("REFUSING")]
    Refusing = 4,

    /// <summary>
    /// Refused
    /// </summary>
    [Map("REFUSED")]
    Refused = 5,

    /// <summary>
    /// Receiving
    /// </summary>
    [Map("RECEIVING")]
    Receiving = 6,

    /// <summary>
    /// Received
    /// </summary>
    [Map("RECEIVED")]
    Received = 7,
}
