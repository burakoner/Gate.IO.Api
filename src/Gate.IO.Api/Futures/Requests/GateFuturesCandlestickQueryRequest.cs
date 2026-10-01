namespace Gate.IO.Api.Futures;

/// <summary>
/// Futures candlestick query request
/// </summary>
public record GateFuturesCandlestickQueryRequest
{
    /// <summary>
    /// Gets or sets the Contract.
    /// </summary>
    public string Contract { get; set; }
    /// <summary>
    /// Gets or sets the Interval.
    /// </summary>
    public GateFuturesCandlestickInterval Interval { get; set; }
    /// <summary>
    /// Gets or sets the From.
    /// </summary>
    public DateTime? From { get; set; }
    /// <summary>
    /// Gets or sets the To.
    /// </summary>
    public DateTime? To { get; set; }
    /// <summary>
    /// Gets or sets the Limit.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>Candlestick timezone: all, utc0 or utc8. Omitted uses the server's utc0 default. Not supported by premium_index.</summary>
    public string Timezone { get; set; }
}
