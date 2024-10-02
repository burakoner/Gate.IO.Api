namespace Gate.IO.Api.SubAccount;

/// <summary>
/// Sub Account Type
/// </summary>
public enum GateSubAccountType
{
    /// <summary>
    /// SubAccount
    /// </summary>
    [Map("1")]
    SubAccount,

    /// <summary>
    /// CrossMarginAccount
    /// </summary>
    [Map("3")]
    CrossMarginAccount,
}