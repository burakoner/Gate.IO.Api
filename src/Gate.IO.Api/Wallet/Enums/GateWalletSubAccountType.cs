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
    Spot,

    /// <summary>
    /// perpetual contract account
    /// </summary>
    [Map("futures")]
    Futures,

    /// <summary>
    /// delivery account
    /// </summary>
    [Map("delivery")]
    Delivery,
}