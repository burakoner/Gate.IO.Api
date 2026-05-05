namespace Gate.IO.Api.Margin;

/// <summary>
/// Gate Margin Uni Loan
/// </summary>
public record GateMarginLoan
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Symbol
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    [JsonProperty("currency_pari")]
    internal string SymbolAlias { set => Symbol = value; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(MapConverter))]
    public GateMarginLoanType Type { get; set; }

    /// <summary>
    /// Create time
    /// </summary>
    [JsonProperty("create_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Update time
    /// </summary>
    [JsonProperty("update_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdateTime { get; set; }

    [JsonProperty("change_time")]
    [JsonConverter(typeof(DateTimeConverter))]
    internal DateTime? ChangeTime { set => UpdateTime = value; }
}
