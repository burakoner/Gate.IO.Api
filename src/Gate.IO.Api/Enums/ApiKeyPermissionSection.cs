namespace Gate.IO.Api.Enums;

/// <summary>
/// Permission name (all permissions will be removed if no value is passed)  - wallet: wallet - spot: spot/margin - futures: perpetual contract - delivery: delivery - earn: earn - options: options
/// </summary>
public enum ApiKeyPermissionSection
{
    /// <summary>
    /// Enum Wallet for value: wallet
    /// </summary>
    [Label("wallet")]
    Wallet,

    /// <summary>
    /// Enum Spot for value: spot
    /// </summary>
    [Label("spot")]
    Spot,

    /// <summary>
    /// Enum Futures for value: futures
    /// </summary>
    [Label("futures")]
    Futures,

    /// <summary>
    /// Enum Delivery for value: delivery
    /// </summary>
    [Label("delivery")]
    Delivery,

    /// <summary>
    /// Enum Earn for value: earn
    /// </summary>
    [Label("earn")]
    Earn,

    /// <summary>
    /// Enum Options for value: options
    /// </summary>
    [Label("options")]
    Options
}