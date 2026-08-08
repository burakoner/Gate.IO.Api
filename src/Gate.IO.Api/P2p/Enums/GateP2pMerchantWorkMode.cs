namespace Gate.IO.Api.P2p;

/// <summary>
/// P2P merchant work mode
/// </summary>
public enum GateP2pMerchantWorkMode : byte
{
    /// <summary>
    /// Resting
    /// </summary>
    Resting = 0,

    /// <summary>
    /// Working
    /// </summary>
    Working = 1,

    /// <summary>
    /// Use custom working hours
    /// </summary>
    CustomHours = 2,
}
