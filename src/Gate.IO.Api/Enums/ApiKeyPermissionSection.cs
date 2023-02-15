namespace Gate.IO.Api.Enums;

/// <summary>
/// Permission name (all permissions will be removed if no value is passed)  - wallet: wallet - spot: spot/margin - futures: perpetual contract - delivery: delivery - earn: earn - options: options
/// </summary>
public enum ApiKeyPermissionSection
{
    /// <summary>
    /// Enum Wallet for value: wallet
    /// </summary>
    [EnumMember(Value = "wallet")]
    Wallet,

    /// <summary>
    /// Enum Spot for value: spot
    /// </summary>
    [EnumMember(Value = "spot")]
    Spot,

    /// <summary>
    /// Enum Futures for value: futures
    /// </summary>
    [EnumMember(Value = "futures")]
    Futures,

    /// <summary>
    /// Enum Delivery for value: delivery
    /// </summary>
    [EnumMember(Value = "delivery")]
    Delivery,

    /// <summary>
    /// Enum Earn for value: earn
    /// </summary>
    [EnumMember(Value = "earn")]
    Earn,

    /// <summary>
    /// Enum Options for value: options
    /// </summary>
    [EnumMember(Value = "options")]
    Options
}