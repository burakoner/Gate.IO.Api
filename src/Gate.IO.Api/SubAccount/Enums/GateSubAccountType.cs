namespace Gate.IO.Api.SubAccount;

/// <summary>
/// Sub Account Type
/// </summary>
public enum GateSubAccountType : byte
{
    /// <summary>
    /// SubAccount
    /// </summary>
    [Map("1")]
    SubAccount = 1,

    /// <summary>
    /// CrossMarginAccount
    /// </summary>
    [Map("3")]
    CrossMarginAccount = 3,
}