namespace Gate.IO.Api.Wallet;

/// <summary>
/// Wallet Sub Account Cross Margin Balance
/// </summary>
public record GateWalletSubAccountCrossMarginBalance
{
    /// <summary>
    /// User ID
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Available
    /// </summary>
    [JsonProperty("available")]
    public GateWalletSubAccountCrossMarginBalanceAvailable Available { get; set; }
}

/// <summary>
/// Wallet Sub Account Cross Margin Balance Available
/// </summary>
public record GateWalletSubAccountCrossMarginBalanceAvailable
{
    /// <summary>
    /// User ID of the cross margin account. 0 means that the subaccount has not yet opened a cross margin account
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    /// <summary>
    /// Whether account is locked
    /// </summary>
    [JsonProperty("locked")]
    public bool Locked { get; set; }

    /// <summary>
    /// Balances
    /// </summary>
    [JsonProperty("balances")]
    public Dictionary<string, GateWalletSubAccountCrossMarginBalanceAvailableItem> Balances { get; set; }

    /// <summary>
    /// Total account value in USDT, i.e., the sum of all currencies' (available+freeze)*price*discount
    /// </summary>
    [JsonProperty("total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Total borrowed value in USDT, i.e., the sum of all currencies' borrowed*price*discount
    /// </summary>
    [JsonProperty("borrowed")]
    public decimal Borrowed { get; set; }

    /// <summary>
    /// Total borrowed value in USDT * borrowed factor
    /// </summary>
    [JsonProperty("borrowed_net")]
    public decimal BorrowedNet { get; set; }

    /// <summary>
    /// Total net assets in USDT
    /// </summary>
    [JsonProperty("net")]
    public decimal Net { get; set; }

    /// <summary>
    /// Position leverage
    /// </summary>
    [JsonProperty("leverage")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Total unpaid interests in USDT, i.e., the sum of all currencies' interest*price*discount
    /// </summary>
    [JsonProperty("interest")]
    public decimal Interest { get; set; }

    /// <summary>
    /// Risk rate. When it belows 110%, liquidation will be triggered. Calculation formula: total / (borrowed+interest)
    /// </summary>
    [JsonProperty("risk")]
    public decimal Risk { get; set; }

    /// <summary>
    /// Total initial margin
    /// </summary>
    [JsonProperty("total_initial_margin")]
    public decimal TotalInitialMargin { get; set; }

    /// <summary>
    /// Total margin balance
    /// </summary>
    [JsonProperty("total_margin_balance")]
    public decimal TotalMarginBalance { get; set; }

    /// <summary>
    /// Total maintenance margin
    /// </summary>
    [JsonProperty("total_maintenance_margin")]
    public decimal TotalMaintenanceMargin { get; set; }

    /// <summary>
    /// Total initial margin rate
    /// </summary>
    [JsonProperty("total_initial_margin_rate")]
    public decimal TotalInitialMarginRate { get; set; }

    /// <summary>
    /// Total maintenance margin rate
    /// </summary>
    [JsonProperty("total_maintenance_margin_rate")]
    public decimal TotalMaintenanceMarginRate { get; set; }

    /// <summary>
    /// Total available margin
    /// </summary>
    [JsonProperty("total_available_margin")]
    public decimal TotalAvailableMargin { get; set; }
}

/// <summary>
/// Wallet Sub Account Cross Margin Balance Available Item
/// </summary>
public record GateWalletSubAccountCrossMarginBalanceAvailableItem
{
    /// <summary>
    /// Available amount
    /// </summary>
    [JsonProperty("available")]
    public decimal Available { get; set; }

    /// <summary>
    /// Locked amount
    /// </summary>
    [JsonProperty("freeze")]
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