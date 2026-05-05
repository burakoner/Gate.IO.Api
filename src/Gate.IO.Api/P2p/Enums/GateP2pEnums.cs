namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P order side
/// </summary>
public enum GateP2pOrderSide : byte
{
    /// <summary>
    /// Buy
    /// </summary>
    [Map("buy")]
    Buy = 1,

    /// <summary>
    /// Sell
    /// </summary>
    [Map("sell")]
    Sell = 2,
}

/// <summary>
/// P2P order tab
/// </summary>
public enum GateP2pOrderTab : byte
{
    /// <summary>
    /// Pending orders
    /// </summary>
    [Map("pending")]
    Pending = 1,

    /// <summary>
    /// Dispute orders
    /// </summary>
    [Map("dispute")]
    Dispute = 2,
}

/// <summary>
/// P2P ad operation type
/// </summary>
public enum GateP2pAdOperationType : byte
{
    /// <summary>
    /// Publish sell ad
    /// </summary>
    [Map("0")]
    PublishSell = 0,

    /// <summary>
    /// Publish buy ad
    /// </summary>
    [Map("1")]
    PublishBuy = 1,

    /// <summary>
    /// Edit sell ad
    /// </summary>
    [Map("2")]
    EditSell = 2,

    /// <summary>
    /// Edit buy ad
    /// </summary>
    [Map("3")]
    EditBuy = 3,
}

/// <summary>
/// P2P ad status update
/// </summary>
public enum GateP2pAdStatusUpdate : byte
{
    /// <summary>
    /// Listed
    /// </summary>
    Listed = 1,

    /// <summary>
    /// Delisted
    /// </summary>
    Delisted = 3,

    /// <summary>
    /// Closed
    /// </summary>
    Closed = 4,
}

/// <summary>
/// P2P chat message type
/// </summary>
public enum GateP2pChatMessageType : byte
{
    /// <summary>
    /// Text message
    /// </summary>
    Text = 0,

    /// <summary>
    /// File message
    /// </summary>
    File = 1,
}
