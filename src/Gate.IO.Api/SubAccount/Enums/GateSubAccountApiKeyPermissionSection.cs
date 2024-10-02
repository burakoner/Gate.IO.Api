namespace Gate.IO.Api.SubAccount;

/// <summary>
/// Permission name (all permissions will be removed if no value is passed)  - wallet: wallet - spot: spot/margin - futures: perpetual contract - delivery: delivery - earn: earn - options: options
/// </summary>
public enum GateSubAccountApiKeyPermissionSection
{
    /// <summary>
    /// Enum Wallet for value: wallet
    /// </summary>
    [Map("wallet")]
    Wallet,

    /// <summary>
    /// Enum Spot for value: spot
    /// </summary>
    [Map("spot")]
    Spot,

    /// <summary>
    /// Enum Futures for value: futures
    /// </summary>
    [Map("futures")]
    Futures,

    /// <summary>
    /// Enum Delivery for value: delivery
    /// </summary>
    [Map("delivery")]
    Delivery,

    /// <summary>
    /// Enum Earn for value: earn
    /// </summary>
    [Map("earn")]
    Earn,

    /// <summary>
    /// Enum Options for value: options
    /// </summary>
    [Map("options")]
    Options,

    /// <summary>
    /// Enum Unified for value: unified
    /// </summary>
    [Map("unified")]
    Unified,

    /// <summary>
    /// Enum Loan for value: loan
    /// </summary>
    [Map("loan")]
    Loan
}