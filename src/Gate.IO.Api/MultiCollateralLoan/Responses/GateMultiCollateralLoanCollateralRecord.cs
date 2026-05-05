namespace Gate.IO.Api.MultiCollateralLoan;

/// <summary>
/// Multi-collateral loan collateral adjustment record
/// </summary>
public record GateMultiCollateralLoanCollateralRecord
{
    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("order_id")]
    public long OrderId { get; set; }

    /// <summary>
    /// Collateral record ID
    /// </summary>
    [JsonProperty("record_id")]
    public long RecordId { get; set; }

    /// <summary>
    /// Collateral ratio before adjustment
    /// </summary>
    [JsonProperty("before_ltv")]
    public decimal BeforeLtv { get; set; }

    /// <summary>
    /// Collateral ratio after adjustment
    /// </summary>
    [JsonProperty("after_ltv")]
    public decimal AfterLtv { get; set; }

    /// <summary>
    /// Operation time
    /// </summary>
    [JsonProperty("operate_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime OperateTime { get; set; }

    /// <summary>
    /// Borrowing currency list
    /// </summary>
    [JsonProperty("borrow_currencies")]
    public List<GateMultiCollateralLoanAmountChange> BorrowCurrencies { get; set; } = [];

    /// <summary>
    /// Collateral currency list
    /// </summary>
    [JsonProperty("collateral_currencies")]
    public List<GateMultiCollateralLoanAmountChange> CollateralCurrencies { get; set; } = [];
}
