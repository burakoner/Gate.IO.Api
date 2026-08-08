namespace Gate.IO.Api.P2p;

/// <summary>
/// Market advertisement list request
/// </summary>
public record GateP2pMarketAdListRequest
{
    /// <summary>
    /// Cryptocurrency symbol
    /// </summary>
    public string Asset { get; set; }

    /// <summary>
    /// Fiat currency
    /// </summary>
    public string FiatUnit { get; set; }

    /// <summary>
    /// Advertisement side
    /// </summary>
    public GateP2pOrderSide? TradeType { get; set; }
}
