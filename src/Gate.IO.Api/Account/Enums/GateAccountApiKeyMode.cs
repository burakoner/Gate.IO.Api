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
    /// Legacy unified mode
    /// </summary>
    [Map("2")]
    LegacyUnifiedAccount = 2,

    /// <summary>
    /// Legacy name for unified account mode
    /// </summary>
    [Map("2")]
    PortfolioMarginAccount = 2,
}
