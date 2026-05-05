namespace Gate.IO.Api.Bot;

/// <summary>
/// Bot strategy position
/// </summary>
public record GateBotPortfolioPosition
{
    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    [JsonProperty("entry_price")]
    public decimal? EntryPrice { get; set; }

    [JsonProperty("quote_amount")]
    public decimal? QuoteAmount { get; set; }

    [JsonProperty("position_value")]
    public decimal? PositionValue { get; set; }

    [JsonProperty("margin")]
    public decimal? Margin { get; set; }

    [JsonProperty("side")]
    public string Side { get; set; }
}
