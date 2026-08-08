namespace Gate.IO.Api.P2p;

/// <summary>
/// Current P2P merchant work status
/// </summary>
public enum GateP2pMerchantWorkStatus : byte
{
    /// <summary>
    /// Normal resting
    /// </summary>
    Resting = 0,

    /// <summary>
    /// Normal working
    /// </summary>
    Working = 1,

    /// <summary>
    /// Resting according to custom working hours
    /// </summary>
    CustomResting = 2,

    /// <summary>
    /// Working according to custom working hours
    /// </summary>
    CustomWorking = 3,
}
