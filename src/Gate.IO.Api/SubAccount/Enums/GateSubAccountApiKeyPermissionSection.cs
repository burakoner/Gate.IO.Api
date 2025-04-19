namespace Gate.IO.Api.SubAccount;

/// <summary>
/// Permission name (all permissions will be removed if no value is passed)  - wallet: wallet - spot: spot/margin - futures: perpetual contract - delivery: delivery - earn: earn - options: options
/// </summary>
public enum GateSubAccountApiKeyPermissionSection : byte
{
    /// <summary>
    /// Enum Wallet for value: wallet
    /// </summary>
    [Map("wallet")]
    Wallet = 1,

    /// <summary>
    /// Enum Spot for value: spot
    /// </summary>
    [Map("spot")]
    Spot = 2,

    /// <summary>
    /// Enum Futures for value: futures
    /// </summary>
    [Map("futures")]
    Futures = 3,

    /// <summary>
    /// Enum Delivery for value: delivery
    /// </summary>
    [Map("delivery")]
    Delivery = 4,

    /// <summary>
    /// Enum Earn for value: earn
    /// </summary>
    [Map("earn")]
    Earn = 5,

    /// <summary>
    /// Custody
    /// </summary>
    [Map("custody")]
    Custody = 6,

    /// <summary>
    /// Enum Options for value: options
    /// </summary>
    [Map("options")]
    Options = 7,

    /// <summary>
    /// Enum Account for value: account
    /// </summary>
    [Map("account")]
    Account = 8,

    /// <summary>
    /// Enum Loan for value: loan
    /// </summary>
    [Map("loan")]
    Loan = 9,

    /// <summary>
    /// Enum Margin for value: margin
    /// </summary>
    [Map("margin")]
    Margin = 10,
    /// <summary>
    /// Enum Unified for value: unified
    /// </summary>
    [Map("unified")]
    Unified = 11,

    /// <summary>
    /// Enum Copy for value: copy
    /// </summary>
    [Map("copy")]
    Copy = 12,
}