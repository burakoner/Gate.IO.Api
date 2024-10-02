namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Account Type
/// </summary>
public enum GateWalletAccountType
{
    /// <summary>
    /// Spot
    /// </summary>
    [Map("spot")]
    Spot,

    /// <summary>
    /// Margin
    /// </summary>
    [Map("margin")]
    Margin,
    
    /// <summary>
    /// Futures
    /// </summary>
    [Map("futures")]
    Futures,

    /// <summary>
    /// Delivery
    /// </summary>
    [Map("delivery")]
    Delivery,

    /// <summary>
    /// Options
    /// </summary>
    [Map("options")]
    Options,
}