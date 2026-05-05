namespace Gate.IO.Api.P2p;

/// <summary>
/// Ad list request
/// </summary>
public record GateP2pAdListRequest
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
    /// Ad side
    /// </summary>
    public GateP2pOrderSide? TradeType { get; set; }
}
