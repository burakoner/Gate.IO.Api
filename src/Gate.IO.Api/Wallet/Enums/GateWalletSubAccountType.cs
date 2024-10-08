namespace Gate.IO.Api.Wallet;

/// <summary>
/// Sub Account Type
/// </summary>
public enum GateWalletSubAccountType
{
    /// <summary>
    /// spot account
    /// </summary>
    [Map("spot")]
    Spot = 1,

    /// <summary>
    /// perpetual contract account
    /// </summary>
    [Map("futures")]
    Futures = 2,

    /// <summary>
    /// delivery account
    /// </summary>
    [Map("delivery")]
    Delivery = 3,
}