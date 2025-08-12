namespace Gate.IO.Api.Margin;

/// <summary>
/// Margin Account Balance
/// </summary>
public record GateMarginBalance
{
    /// <summary>
    /// Currency pair
    /// </summary>
    [JsonProperty("currency_pair")]
    public string Symbol { get; set; }

    /// <summary>
    /// Account type, risk - risk rate account, mmr - maintenance margin rate account, inactive - market not activated
    /// </summary>
    [JsonProperty("account_type")]
    public string AccountType { get; set; }

    /// <summary>
    /// User current market leverage multiple
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Whether account is locked
    /// </summary>
    [JsonProperty("locked")]
    public bool Locked { get; set; }

    /// <summary>
    /// Current risk rate of margin account
    /// </summary>
    [JsonProperty("risk")]
    public decimal Risk { get; set; }

    /// <summary>
    /// Leveraged Account Current Maintenance Margin Rate (returned when the Account is a Maintenance Margin Rate Account)
    /// </summary>
    [JsonProperty("mmr")]
    public decimal MMR { get; set; }

    /// <summary>
    /// Account currency details
    /// </summary>
    [JsonProperty("base")]
    public GateMarginBalanceData Base { get; set; }

    /// <summary>
    /// Account currency details
    /// </summary>
    [JsonProperty("quote")]
    public GateMarginBalanceData Quote { get; set; }
}

/// <summary>
/// Gate.IO Margin Balance Data
/// </summary>
public record GateMarginBalanceData
{
    /// <summary>
    /// Currency name
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount suitable for margin trading.
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked amount, used in margin trading
    /// </summary>
    [JsonProperty("locked")]
    public decimal Locked { get; set; }

    /// <summary>
    /// Borrowed amount
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Unpaid interests
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }
}
