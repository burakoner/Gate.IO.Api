namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan order
/// </summary>
public record GateMultiCollateralLoanOrder
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Order type
    /// </summary>
    [JsonProperty("order_type")]
    [JsonConverter(typeof(MapConverter))]
    public GateMultiCollateralLoanOrderType OrderType { get; set; }

    /// <summary>
    /// Fixed interest rate loan period
    /// </summary>
    [JsonProperty("fixed_type")]
    [JsonConverter(typeof(MapConverter))]
    public GateMultiCollateralLoanFixedType? FixedType { get; set; }

    /// <summary>
    /// Fixed interest rate
    /// </summary>
    [JsonProperty("fixed_rate")]
    public decimal? FixedRate { get; set; }

    /// <summary>
    /// Expiration time
    /// </summary>
    [JsonProperty("expire_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// Fixed interest rate auto-renewal
    /// </summary>
    [JsonProperty("auto_renew")]
    public bool AutoRenew { get; set; }

    /// <summary>
    /// Fixed interest rate auto-repayment
    /// </summary>
    [JsonProperty("auto_repay")]
    public bool AutoRepay { get; set; }

    /// <summary>
    /// Current collateralization rate
    /// </summary>
    [JsonProperty("current_ltv")]
    public decimal CurrentLtv { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    [JsonProperty("status")]
    [JsonConverter(typeof(MapConverter))]
    public GateMultiCollateralLoanOrderStatus Status { get; set; }

    /// <summary>
    /// Borrowing time
    /// </summary>
    [JsonProperty("borrow_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime BorrowTime { get; set; }

    /// <summary>
    /// Total outstanding value converted to USDT
    /// </summary>
    [JsonProperty("total_left_repay_usdt")]
    public decimal TotalLeftRepayUsdt { get; set; }

    /// <summary>
    /// Total collateral value converted to USDT
    /// </summary>
    [JsonProperty("total_left_collateral_usdt")]
    public decimal TotalLeftCollateralUsdt { get; set; }

    /// <summary>
    /// Borrowing currency list
    /// </summary>
    [JsonProperty("borrow_currencies")]
    public List<GateMultiCollateralLoanBorrowCurrency> BorrowCurrencies { get; set; } = [];

    /// <summary>
    /// Collateral currency list
    /// </summary>
    [JsonProperty("collateral_currencies")]
    public List<GateMultiCollateralLoanCollateralCurrency> CollateralCurrencies { get; set; } = [];
}

/// <summary>
/// Multi-collateral loan borrowed currency details
/// </summary>
public record GateMultiCollateralLoanBorrowCurrency
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
    /// Outstanding principal
    /// </summary>
    [JsonProperty("left_repay_principal")]
    public decimal LeftRepayPrincipal { get; set; }

    /// <summary>
    /// Outstanding interest
    /// </summary>
    [JsonProperty("left_repay_interest")]
    public decimal LeftRepayInterest { get; set; }

    /// <summary>
    /// Remaining total outstanding value converted to USDT
    /// </summary>
    [JsonProperty("left_repay_usdt")]
    public decimal LeftRepayUsdt { get; set; }
}

/// <summary>
/// Multi-collateral loan collateral currency details
/// </summary>
public record GateMultiCollateralLoanCollateralCurrency
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
    /// Remaining collateral amount
    /// </summary>
    [JsonProperty("left_collateral")]
    public decimal LeftCollateral { get; set; }

    /// <summary>
    /// Remaining collateral value converted to USDT
    /// </summary>
    [JsonProperty("left_collateral_usdt")]
    public decimal LeftCollateralUsdt { get; set; }
}
