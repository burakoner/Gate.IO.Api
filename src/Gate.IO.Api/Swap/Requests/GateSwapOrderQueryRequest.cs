namespace Gate.IO.Api.Swap;

/// <summary>
/// Flash swap order query request
/// </summary>
public record GateSwapOrderQueryRequest
{
    /// <summary>
    /// Flash swap order status
    /// </summary>
    public GateSwapOrderStatus? Status { get; set; }

    /// <summary>
    /// Asset name to sell
    /// </summary>
    public string SellCurrency { get; set; }

    /// <summary>
    /// Asset name to buy
    /// </summary>
    public string BuyCurrency { get; set; }

    /// <summary>
    /// Sort by ID in descending order when true
    /// </summary>
    public bool? Reverse { get; set; }

    /// <summary>
    /// Maximum number of records returned in a single list
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int? Page { get; set; }
}
