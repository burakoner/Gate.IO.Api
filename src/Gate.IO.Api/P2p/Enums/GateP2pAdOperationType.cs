namespace Gate.IO.Api.P2p;

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
