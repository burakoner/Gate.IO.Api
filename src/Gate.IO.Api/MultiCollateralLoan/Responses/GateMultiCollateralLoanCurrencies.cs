namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Borrowing and collateral currencies supported for multi-collateral loans
/// </summary>
public record GateMultiCollateralLoanCurrencies
{
    /// <summary>
    /// Supported borrowing currencies
    /// </summary>
    [JsonProperty("loan_currencies")]
    public List<GateMultiCollateralLoanCurrency> LoanCurrencies { get; set; } = [];

    /// <summary>
    /// Supported collateral currencies
    /// </summary>
    [JsonProperty("collateral_currencies")]
    public List<GateMultiCollateralLoanSupportedCollateralCurrency> CollateralCurrencies { get; set; } = [];
}

/// <summary>
/// Multi-collateral loan supported borrowing currency
/// </summary>
public record GateMultiCollateralLoanCurrency
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Latest price of the currency
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }
}

/// <summary>
/// Multi-collateral loan supported collateral currency
/// </summary>
public record GateMultiCollateralLoanSupportedCollateralCurrency
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Currency index price
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }

    /// <summary>
    /// Discount
    /// </summary>
    [JsonProperty("discount")]
    public decimal Discount { get; set; }
}
