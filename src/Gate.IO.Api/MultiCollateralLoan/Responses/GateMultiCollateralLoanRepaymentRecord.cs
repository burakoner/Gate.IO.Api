namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan repayment record
/// </summary>
public record GateMultiCollateralLoanRepaymentRecord
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Repayment record ID
    /// </summary>
    [JsonProperty("record_id")]
    public long RecordId { get; set; }

    /// <summary>
    /// Initial collateralization rate
    /// </summary>
    [JsonProperty("init_ltv")]
    public decimal InitialLtv { get; set; }

    /// <summary>
    /// LTV before the operation
    /// </summary>
    [JsonProperty("before_ltv")]
    public decimal BeforeLtv { get; set; }

    /// <summary>
    /// LTV after the operation
    /// </summary>
    [JsonProperty("after_ltv")]
    public decimal AfterLtv { get; set; }

    /// <summary>
    /// Borrowing time
    /// </summary>
    [JsonProperty("borrow_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime BorrowTime { get; set; }

    /// <summary>
    /// Repayment time
    /// </summary>
    [JsonProperty("repay_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime RepayTime { get; set; }

    /// <summary>
    /// Borrowing information
    /// </summary>
    [JsonProperty("borrow_currencies")]
    public List<GateMultiCollateralLoanAmountChange> BorrowCurrencies { get; set; } = [];

    /// <summary>
    /// Collateral information
    /// </summary>
    [JsonProperty("collateral_currencies")]
    public List<GateMultiCollateralLoanAmountChange> CollateralCurrencies { get; set; } = [];

    /// <summary>
    /// Repaid currencies
    /// </summary>
    [JsonProperty("repaid_currencies")]
    public List<GateMultiCollateralLoanRepaidCurrencyRecord> RepaidCurrencies { get; set; } = [];

    /// <summary>
    /// Total interest list
    /// </summary>
    [JsonProperty("total_interest_list")]
    public List<GateMultiCollateralLoanInterestAmount> TotalInterestList { get; set; } = [];

    /// <summary>
    /// Remaining interest to be repaid
    /// </summary>
    [JsonProperty("left_repay_interest_list")]
    public List<GateMultiCollateralLoanInterestChange> LeftRepayInterestList { get; set; } = [];
}

/// <summary>
/// Multi-collateral loan before and after amount values
/// </summary>
public record GateMultiCollateralLoanAmountChange
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
    /// Amount before the operation
    /// </summary>
    [JsonProperty("before_amount")]
    public decimal BeforeAmount { get; set; }

    /// <summary>
    /// USDT amount before the operation
    /// </summary>
    [JsonProperty("before_amount_usdt")]
    public decimal BeforeAmountUsdt { get; set; }

    /// <summary>
    /// Amount after the operation
    /// </summary>
    [JsonProperty("after_amount")]
    public decimal AfterAmount { get; set; }

    /// <summary>
    /// USDT amount after the operation
    /// </summary>
    [JsonProperty("after_amount_usdt")]
    public decimal AfterAmountUsdt { get; set; }
}

/// <summary>
/// Multi-collateral loan repaid currency record
/// </summary>
public record GateMultiCollateralLoanRepaidCurrencyRecord
{
    /// <summary>
    /// Repayment currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Currency index price
    /// </summary>
    [JsonProperty("index_price")]
    public decimal IndexPrice { get; set; }

    /// <summary>
    /// Repayment amount
    /// </summary>
    [JsonProperty("repaid_amount")]
    public decimal RepaidAmount { get; set; }

    /// <summary>
    /// Principal
    /// </summary>
    [JsonProperty("repaid_principal")]
    public decimal RepaidPrincipal { get; set; }

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("repaid_interest")]
    public decimal RepaidInterest { get; set; }

    /// <summary>
    /// Repayment amount converted to USDT
    /// </summary>
    [JsonProperty("repaid_amount_usdt")]
    public decimal RepaidAmountUsdt { get; set; }
}

/// <summary>
/// Multi-collateral loan interest amount
/// </summary>
public record GateMultiCollateralLoanInterestAmount
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
    /// Interest amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Interest amount converted to USDT
    /// </summary>
    [JsonProperty("amount_usdt")]
    public decimal AmountUsdt { get; set; }
}

/// <summary>
/// Multi-collateral loan interest amount change
/// </summary>
public record GateMultiCollateralLoanInterestChange
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
    /// Interest amount before repayment
    /// </summary>
    [JsonProperty("before_amount")]
    public decimal BeforeAmount { get; set; }

    /// <summary>
    /// Converted value of interest before repayment in USDT
    /// </summary>
    [JsonProperty("before_amount_usdt")]
    public decimal BeforeAmountUsdt { get; set; }

    /// <summary>
    /// Interest amount after repayment
    /// </summary>
    [JsonProperty("after_amount")]
    public decimal AfterAmount { get; set; }

    /// <summary>
    /// Converted value of interest after repayment in USDT
    /// </summary>
    [JsonProperty("after_amount_usdt")]
    public decimal AfterAmountUsdt { get; set; }
}
