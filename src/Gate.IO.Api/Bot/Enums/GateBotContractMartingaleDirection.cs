namespace Gate.IO.Api.Bot;

/// <summary>
/// Contract martingale direction
/// </summary>
public enum GateBotContractMartingaleDirection : byte
{
    [Map("buy")]
    Buy = 1,

    [Map("sell")]
    Sell = 2,
}
