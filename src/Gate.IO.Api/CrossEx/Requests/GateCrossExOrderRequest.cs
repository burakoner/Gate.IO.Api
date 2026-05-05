namespace Gate.IO.Api.CrossEx;

/// <summary>
/// CrossEx order request
/// </summary>
public record GateCrossExOrderRequest
{
    /// <summary>
    /// Client-defined order ID
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Trading pair identifier
    /// </summary>
    public string Symbol { get; set; }

    /// <summary>
    /// Order side
    /// </summary>
    public GateCrossExOrderSide Side { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    public GateCrossExOrderType? Type { get; set; }

    /// <summary>
    /// Time in force
    /// </summary>
    public GateCrossExTimeInForce? TimeInForce { get; set; }

    /// <summary>
    /// Base currency order quantity
    /// </summary>
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Limit order price
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Quote currency order quantity
    /// </summary>
    public decimal? QuoteQuantity { get; set; }

    /// <summary>
    /// Reduce-only flag
    /// </summary>
    public bool? ReduceOnly { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    public GateCrossExPositionSide? PositionSide { get; set; }
}
