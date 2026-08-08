namespace Gate.IO.Api.TradFi;

/// <summary>
/// TradFi symbol commission rate
/// </summary>
public record GateTradFiSymbolCommission
{
    /// <summary>
    /// Gets or sets the Category Code.
    /// </summary>
    [JsonProperty("category_code")]
    public string CategoryCode { get; set; }

    /// <summary>
    /// Gets or sets the Trading Symbol Code.
    /// </summary>
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    /// <summary>
    /// Gets or sets the Commission Rate Per Lot.
    /// </summary>
    [JsonProperty("fee_per_lot")]
    public decimal FeePerLot { get; set; }
}
