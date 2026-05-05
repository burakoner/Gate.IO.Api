namespace Gate.IO.Api.Alpha;

/// <summary>
/// Alpha quote response.
/// </summary>
public record GateAlphaQuote
{
    /// <summary>
    /// Quote ID for order placement, valid for one minute.
    /// </summary>
    [JsonProperty("quote_id")]
    public string QuoteId { get; set; }

    /// <summary>
    /// Minimum order size.
    /// </summary>
    [JsonProperty("min_amount")]
    public decimal MinimumAmount { get; set; }

    /// <summary>
    /// Maximum order size.
    /// </summary>
    [JsonProperty("max_amount")]
    public decimal MaximumAmount { get; set; }

    /// <summary>
    /// Token price in USDT.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Slippage.
    /// </summary>
    [JsonProperty("slippage")]
    public decimal Slippage { get; set; }

    /// <summary>
    /// Estimated gas fee in USDT, as returned by Gate. This field may include a currency symbol.
    /// </summary>
    [JsonProperty("estimate_gas_fee_amount_usdt")]
    public string EstimatedGasFeeAmountUsdt { get; set; }

    /// <summary>
    /// Trading fee, as returned by Gate. This field may include a currency symbol.
    /// </summary>
    [JsonProperty("order_fee")]
    public string OrderFee { get; set; }

    /// <summary>
    /// Minimum received target token amount.
    /// </summary>
    [JsonProperty("target_token_min_amount")]
    public decimal TargetTokenMinimumAmount { get; set; }

    /// <summary>
    /// Maximum received target token amount.
    /// </summary>
    [JsonProperty("target_token_max_amount")]
    public decimal TargetTokenMaximumAmount { get; set; }

    /// <summary>
    /// Quote error type.
    /// </summary>
    [JsonProperty("error_type")]
    public GateAlphaQuoteErrorType ErrorType { get; set; }
}
