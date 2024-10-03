namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Cross Margin Currency
/// </summary>
public class GateCrossMarginCurrency
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Loan rate
    /// </summary>
    [JsonProperty("rate")]
    public decimal Rate { get; set; }

    /// <summary>
    /// Currency precision
    /// </summary>
    [JsonProperty("prec")]
    public decimal Precision { get; set; }

    /// <summary>
    /// Currency value discount, which is used in total value calculation
    /// </summary>
    [JsonProperty("discount")]
    public decimal Discount { get; set; }

    /// <summary>
    /// Minimum currency borrow amount. Unit is currency itself
    /// </summary>
    [JsonProperty("min_borrow_amount")]
    public decimal MinimumBorrowAmount { get; set; }

    /// <summary>
    /// Maximum borrow value allowed per user, in USDT
    /// </summary>
    [JsonProperty("user_max_borrow_amount")]
    public decimal UserMaximumBorrowAmount { get; set; }

    /// <summary>
    /// Maximum borrow value allowed for this currency, in USDT
    /// </summary>
    [JsonProperty("total_max_borrow_amount")]
    public decimal TotalMaximumBorrowAmount { get; set; }

    /// <summary>
    /// Price change between this currency and USDT
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Whether currency is borrowed
    /// </summary>
    [JsonProperty("loanable")]
    public bool Loanable { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    [JsonProperty("status")]
    public GateMarginMarketStatus Status { get; set; }
}
