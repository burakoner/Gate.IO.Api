namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Account Type
/// </summary>
public enum GateWalletAccountType : byte
{
    /// <summary>
    /// Spot
    /// </summary>
    [Map("spot")]
    Spot = 1,

    /// <summary>
    /// Margin
    /// </summary>
    [Map("margin")]
    Margin = 2,
    
    /// <summary>
    /// Futures
    /// </summary>
    [Map("futures")]
    Futures = 3,

    /// <summary>
    /// Delivery
    /// </summary>
    [Map("delivery")]
    Delivery = 4,

    /// <summary>
    /// Options
    /// </summary>
    [Map("options")]
    Options = 5,
}