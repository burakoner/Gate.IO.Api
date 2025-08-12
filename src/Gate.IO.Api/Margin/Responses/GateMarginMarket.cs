namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Uni Market
/// </summary>
public record GateMarginMarket
{
    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Minimum Base Borrow Quantity
    /// </summary>
    [JsonProperty("base_min_borrow_amount")]
    public decimal MinimumBaseBorrowQuantity { get; set; }

    /// <summary>
    /// Minimum Quote Borrow Quantity
    /// </summary>
    [JsonProperty("quote_min_borrow_amount")]
    public decimal MinimumQuoteBorrowQuantity { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }
}
