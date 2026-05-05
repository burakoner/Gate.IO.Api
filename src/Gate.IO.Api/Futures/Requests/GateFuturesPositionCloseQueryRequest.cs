namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures position-close history query request
/// </summary>
public record GateFuturesPositionCloseQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the From.
    /// </summary>
    public DateTime? From { get; set; }
    /// <summary>
    /// Gets or sets the To.
    /// </summary>
    public DateTime? To { get; set; }
    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    public GateFuturesPositionSide? Side { get; set; }
    /// <summary>
    /// Gets or sets the PnL.
    /// </summary>
    public decimal? Pnl { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }
    /// <summary>
    /// Gets or sets the Offset.
    /// </summary>
    public int? Offset { get; set; }
}
