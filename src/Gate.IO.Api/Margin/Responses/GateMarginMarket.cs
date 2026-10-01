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
    /// Minimum borrow amount of the base currency. The wire value is a decimal string.
    /// </summary>
    [JsonProperty("base_min_borrow_amount")]
    public decimal MinimumBaseBorrowQuantity { get; set; }

    /// <summary>
    /// Minimum borrow amount of the quote currency. The wire value is a decimal string.
    /// </summary>
    [JsonProperty("quote_min_borrow_amount")]
    public decimal MinimumQuoteBorrowQuantity { get; set; }

    /// <summary>
    /// Leverage multiplier. The wire value is a decimal string.
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Market status: enabled or disabled. An omitted/null status remains null, and other strings are preserved.
    /// This is independent of the delisting time; do not infer that a disabled market is delisted.
    /// </summary>
    [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
    public string Status { get; set; }

    /// <summary>
    /// Raw int64 delisting time, when returned. The current endpoint contract does not specify its unit or zero semantics,
    /// so no DateTime conversion or sentinel interpretation is applied. An omitted/null value remains null.
    /// </summary>
    [JsonProperty("delisted_time", NullValueHandling = NullValueHandling.Ignore)]
    public long? DelistedTime { get; set; }
}
