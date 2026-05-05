namespace Gate.IO.Api.P2p;

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
