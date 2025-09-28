namespace Gate.IO.Api.Unified;

/// <summary>
/// Gate Unified Currency
/// </summary>
public record GateUnifiedCurrency
{
    /// <summary>
    /// Name
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Currency precision
    /// </summary>
    [JsonProperty("prec")]
    public decimal Precision { get; set; }

    /// <summary>
    /// Minimum borrowable limit, in currency units
    /// </summary>
    [JsonProperty("min_borrow_amount")]
    public decimal MinimumBorrowAmount { get; set; }

    /// <summary>
    /// User's maximum borrowable limit, in USDT
    /// </summary>
    [JsonProperty("user_max_borrow_amount")]
    public decimal UserMaximumBorrowAmount { get; set; }

    /// <summary>
    /// Platform's maximum borrowable limit, in USDT
    /// </summary>
    [JsonProperty("total_max_borrow_amount")]
    public decimal TotalMaximumBorrowAmount { get; set; }

    /// <summary>
    /// Lending status
    /// - disable : Lending prohibited
    /// - enable : Lending supported
    /// </summary>
    [JsonProperty("loan_status"), JsonConverter(typeof(MapConverter))]
    public GateUnifiedLendingStatus Status { get; set; }
}
