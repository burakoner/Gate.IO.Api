namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy position
/// </summary>
public record GateBotPortfolioPosition
{
    /// <summary>
    /// Gets or sets the Amount.
    /// </summary>
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    /// <summary>
    /// Gets or sets the Entry Price.
    /// </summary>
    [JsonProperty("entry_price")]
    public decimal? EntryPrice { get; set; }

    /// <summary>
    /// Gets or sets the Quote Amount.
    /// </summary>
    [JsonProperty("quote_amount")]
    public decimal? QuoteAmount { get; set; }

    /// <summary>
    /// Gets or sets the Position Value.
    /// </summary>
    [JsonProperty("position_value")]
    public decimal? PositionValue { get; set; }

    /// <summary>
    /// Gets or sets the Margin.
    /// </summary>
    [JsonProperty("margin")]
    public decimal? Margin { get; set; }

    /// <summary>
    /// Gets or sets the Side.
    /// </summary>
    [JsonProperty("side")]
    public string Side { get; set; }
}
