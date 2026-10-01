namespace Gate.IO.Api.Futures;

/// <summary>Private liquidation history. Separate from public liq_orders filters.</summary>
public record GateFuturesUserLiquidationQueryRequest
{
    /// <summary>Optional contract filter.</summary>
    public string Contract { get; set; }
    /// <summary>Optional range start.</summary>
    public DateTime? From { get; set; }
    /// <summary>Optional range end.</summary>
    public DateTime? To { get; set; }
    /// <summary>Optional liquidation timestamp.</summary>
    public DateTime? At { get; set; }
    /// <summary>Optional maximum record count.</summary>
    public int? Limit { get; set; }
    /// <summary>Optional zero-based offset.</summary>
    public int? Offset { get; set; }
}
