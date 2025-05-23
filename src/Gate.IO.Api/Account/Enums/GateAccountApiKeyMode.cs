namespace Gate.IO.Api.Account;

/// <summary>
/// GateAccountApiKeyMode
/// </summary>
public enum GateAccountApiKeyMode : byte
{
    /// <summary>
    /// Classic Account
    /// </summary>
    [Map("1")]
    ClassicAccount = 1,

    /// <summary>
    /// portfolio margin account
    /// </summary>
    [Map("2")]
    PortfolioMarginAccount = 2,
}