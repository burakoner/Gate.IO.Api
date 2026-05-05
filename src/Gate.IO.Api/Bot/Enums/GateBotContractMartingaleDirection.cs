namespace Gate.IO.Api.Bot;

/// <summary>
/// Contract martingale direction
/// </summary>
public enum GateBotContractMartingaleDirection : byte
{
    /// <summary>
    /// Represents the Buy value.
    /// </summary>
    [Map("buy")]
    Buy = 1,

    /// <summary>
    /// Represents the Sell value.
    /// </summary>
    [Map("sell")]
    Sell = 2,
}
