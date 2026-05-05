namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi transaction request
/// </summary>
public record GateTradFiTransactionRequest
{
    /// <summary>
    /// Gets or sets the Asset.
    /// </summary>
    public string Asset { get; set; }
    /// <summary>
    /// Gets or sets the Change.
    /// </summary>
    public decimal Change { get; set; }
    /// <summary>
    /// Gets or sets the Type.
    /// </summary>
    public GateTradFiTransactionType Type { get; set; }
}
