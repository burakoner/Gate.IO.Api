namespace Gate.IO.Api.Account;

/// <summary>
/// Account API key permission section
/// </summary>
public enum GateAccountApiKeyPermissionSection : byte
{
    /// <summary>
    /// Wallet
    /// </summary>
    [Map("wallet")]
    Wallet = 1,

    /// <summary>
    /// Spot and margin
    /// </summary>
    [Map("spot")]
    Spot = 2,

    /// <summary>
    /// Perpetual futures
    /// </summary>
    [Map("futures")]
    Futures = 3,

    /// <summary>
    /// Delivery futures
    /// </summary>
    [Map("delivery")]
    Delivery = 4,

    /// <summary>
    /// Earn
    /// </summary>
    [Map("earn")]
    Earn = 5,

    /// <summary>
    /// Custody
    /// </summary>
    [Map("custody")]
    Custody = 6,

    /// <summary>
    /// Options
    /// </summary>
    [Map("options")]
    Options = 7,

    /// <summary>
    /// Account information
    /// </summary>
    [Map("account")]
    Account = 8,

    /// <summary>
    /// Lending
    /// </summary>
    [Map("loan")]
    Loan = 9,

    /// <summary>
    /// Margin
    /// </summary>
    [Map("margin")]
    Margin = 10,

    /// <summary>
    /// Unified account
    /// </summary>
    [Map("unified")]
    Unified = 11,

    /// <summary>
    /// Copy trading
    /// </summary>
    [Map("copy")]
    Copy = 12,

    /// <summary>
    /// Pilot
    /// </summary>
    [Map("pilot")]
    Pilot = 13,

    /// <summary>
    /// OTC
    /// </summary>
    [Map("otc")]
    Otc = 14,

    /// <summary>
    /// Alpha
    /// </summary>
    [Map("alpha")]
    Alpha = 15,

    /// <summary>
    /// Cross-exchange
    /// </summary>
    [Map("crossx")]
    CrossExchange = 16,
}
