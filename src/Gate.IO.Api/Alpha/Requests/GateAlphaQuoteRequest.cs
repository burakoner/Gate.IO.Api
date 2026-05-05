namespace Gate.IO.Api.Alpha;

/// <summary>
/// Request to retrieve an Alpha trade quote.
/// </summary>
public record GateAlphaQuoteRequest
{
    /// <summary>
    /// Trading symbol.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Buy or sell side.
    /// </summary>
    public GateAlphaOrderSide Side { get; set; }

    /// <summary>
    /// Trade quantity. For buy orders this refers to USDT; for sell orders this refers to the base currency.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Trading mode that affects slippage selection.
    /// </summary>
    public GateAlphaGasMode GasMode { get; set; }

    /// <summary>
    /// Slippage tolerance, where 10 means 10 percent tolerance.
    /// </summary>
    public decimal? Slippage { get; set; }
}
