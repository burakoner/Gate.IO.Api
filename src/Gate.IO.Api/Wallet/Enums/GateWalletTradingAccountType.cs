namespace Gate.IO.Api.Wallet;

/// <summary>
/// Account type reported for a trading account transfer
/// </summary>
public enum GateWalletTradingAccountType : byte
{
    /// <summary>Spot account</summary>
    [Map("spot")]
    Spot = 1,

    /// <summary>Margin account</summary>
    [Map("margin")]
    Margin = 2,

    /// <summary>Perpetual futures account</summary>
    [Map("futures")]
    Futures = 3,

    /// <summary>Delivery futures account</summary>
    [Map("delivery")]
    Delivery = 4,

    /// <summary>Options account</summary>
    [Map("options")]
    Options = 5,

    /// <summary>Unrecognized account type reported by Gate</summary>
    [Map("unknown")]
    Unknown = 6,
}
